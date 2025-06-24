using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FlowaStudy.Application.AssetTransactions.CreateAssetTransaction;
using FlowaStudy.Application.Cache;
using FlowaStudy.Application.Messaging.Interfaces;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using Moq;
using Xunit;

namespace FlowaStudy.UnitTests.Application.AssetTransactions
{
    public class CreateAssetTransactionHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IAssetTransactionRepository> _assetTransactionRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IAssetRepository> _assetRepoMock = new();
        private readonly Mock<IKafkaProducer> _kafkaProducerMock = new();
        private readonly Mock<ICacheService> _cacheServiceMock = new();

        private readonly CreateAssetTransactionHandler _handler;

        public CreateAssetTransactionHandlerTests()
        {
            _handler = new CreateAssetTransactionHandler(
                _mapperMock.Object,
                _assetTransactionRepoMock.Object,
                _userRepoMock.Object,
                _assetRepoMock.Object,
                _kafkaProducerMock.Object,
                _cacheServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldProcessTransactionSuccessfully_WhenDataIsValid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var assetId = Guid.NewGuid();
            var command = new CreateAssetTransactionCommand(userId, assetId, AssetTransactionType.Buy, 2);

            var user = new User("Lucas", "Lucas@teste.com", 10000m);
            var asset = new Asset("BTC", "Bitcoin", 5000m);
            var transaction = new AssetTransaction(userId, assetId, AssetTransactionType.Buy, 2, 5000m);
            var result = new CreateAssetTransactionResult { Id = transaction.Id };

            _userRepoMock
                .Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _cacheServiceMock
                .Setup(c => c.GetPriceAsync(assetId))
                .ReturnsAsync((decimal?)null);

            _assetRepoMock
                .Setup(r => r.GetByIdAsync(assetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(asset);

            _cacheServiceMock
                .Setup(c => c.SetPriceAsync(assetId, asset.CurrentPrice, It.IsAny<TimeSpan?>()))
                .Returns(Task.CompletedTask);

            _assetTransactionRepoMock
                .Setup(r => r.CreateAsync(It.IsAny<AssetTransaction>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transaction);

            _userRepoMock
                .Setup(r => r.UpdateAsync(user, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _kafkaProducerMock
                .Setup(p => p.SendAsync("asset-transactions", It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<CreateAssetTransactionResult>(transaction))
                .Returns(result);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(transaction.Id);
        }
    }
}
