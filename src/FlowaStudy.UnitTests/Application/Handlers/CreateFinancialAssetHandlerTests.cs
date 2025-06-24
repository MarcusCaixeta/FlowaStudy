using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FlowaStudy.Application.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using Moq;
using Xunit;

namespace FlowaStudy.UnitTests.Application.FinancialAssets
{
    public class CreateFinancialAssetHandlerTests
    {
        private readonly Mock<IFinancialAssetRepository> _financialAssetRepoMock = new();
        private readonly Mock<IFinancialAssetRepositoryMongo> _financialAssetRepoMongoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private readonly CreateFinancialAssetHandler _handler;

        public CreateFinancialAssetHandlerTests()
        {
            _handler = new CreateFinancialAssetHandler(
                _financialAssetRepoMock.Object,
                _mapperMock.Object,
                _financialAssetRepoMongoMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateFinancialAssetAndReturnResult()
        {
            // Arrange
            var command = new CreateFinancialAssetCommand("Tesouro Direto", 1000m, DateTime.Today);
            var entity = new FinancialAsset(command.Name, command.Value, command.AcquisitionDate);
            var createdEntity = new FinancialAsset(command.Name, command.Value, command.AcquisitionDate);
            var result = new CreateFinancialAssetResult { Id = createdEntity.Id };

            _mapperMock.Setup(m => m.Map<CreateFinancialAssetCommand>(It.IsAny<CreateFinancialAssetCommand>()))
                       .Returns(command);

            _financialAssetRepoMock.Setup(r => r.CreateAsync(It.IsAny<FinancialAsset>(), It.IsAny<CancellationToken>()))
                                    .ReturnsAsync(createdEntity);

            _financialAssetRepoMongoMock.Setup(m => m.InsertMongoDbAsync(It.IsAny<FinancialAssetMongo>()))
                                         .Returns(Task.CompletedTask);

            _mapperMock.Setup(m => m.Map<CreateFinancialAssetResult>(createdEntity))
                       .Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(result.Id);
        }
    }
}
