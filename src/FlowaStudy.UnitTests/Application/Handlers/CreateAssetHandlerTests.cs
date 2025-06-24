using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FlowaStudy.Application.Assets.CreateAsset;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using Moq;
using Xunit;

namespace FlowaStudy.UnitTests.Application.Assets
{
    public class CreateAssetHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAssetRepository> _assetRepositoryMock;
        private readonly CreateAssetHandler _handler;

        public CreateAssetHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _assetRepositoryMock = new Mock<IAssetRepository>();
            _handler = new CreateAssetHandler(_mapperMock.Object, _assetRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateAssetAndReturnResultWithIdOnly()
        {
            // Arrange
            var command = new CreateAssetCommand("BTC", "Bitcoin", 200000m);

            var asset = new Asset(command.Symbol, command.Name, command.CurrentPrice);
            var result = new CreateAssetResult { Id = asset.Id };

            _mapperMock.Setup(m => m.Map<CreateAssetCommand>(It.IsAny<CreateAssetCommand>()))
                       .Returns(command);

            _assetRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>()))
                                .ReturnsAsync(asset);

            _mapperMock.Setup(m => m.Map<CreateAssetResult>(It.IsAny<Asset>()))
                       .Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(asset.Id);

            _assetRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Asset>(), It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(m => m.Map<CreateAssetResult>(asset), Times.Once);
        }

    }
}
