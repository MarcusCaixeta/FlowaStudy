using AutoMapper;
using FlowaStudy.Application.Cache;
using FlowaStudy.Application.Messaging.Interfaces;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using MediatR;
using System.Text.Json;
using System.Threading;

namespace FlowaStudy.Application.AssetTransactions.CreateAssetTransaction
{
    public class CreateAssetTransactionHandler : IRequestHandler<CreateAssetTransactionCommand, CreateAssetTransactionResult>
    {
        private readonly IMapper _mapper;
        private readonly IAssetTransactionRepository _assetTransactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IKafkaProducer _kafkaProducer;
        private readonly ICacheService _cacheService;



        public CreateAssetTransactionHandler(IMapper mapper, IAssetTransactionRepository assetTransactionRepository, IUserRepository userRepository
            , IAssetRepository assetRepository, IKafkaProducer kafkaProducer, ICacheService cacheService)
        {
            _mapper = mapper;
            _assetTransactionRepository = assetTransactionRepository;
            _userRepository = userRepository;
            _assetRepository = assetRepository;
            _kafkaProducer = kafkaProducer;
            _cacheService = cacheService;
        }
        public async Task<CreateAssetTransactionResult> Handle(CreateAssetTransactionCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado");
            }

            decimal CurrentPrice = await GetPriceAtExecution(request.AssetId, cancellationToken);

            user.Balance = await GetUpdatedBalanceUser(user.Balance, CurrentPrice, request.Quantity, request.Type);

            var createdAssetTransacion = await CreateAssetTransaction(request, CurrentPrice);

            await _userRepository.UpdateAsync(user);

            KafkaProducerLog(createdAssetTransacion);

            var result = _mapper.Map<CreateAssetTransactionResult>(createdAssetTransacion);

            return result;
        }

        private async Task<decimal> GetPriceAtExecution(Guid AssetId, CancellationToken cancellationToken)
        {
            var cachedPrice = await _cacheService.GetPriceAsync(AssetId);
            decimal CurrentPrice;
            if (cachedPrice == null)
            {
                var asset = await _assetRepository.GetByIdAsync(AssetId, cancellationToken);
                CurrentPrice = asset.CurrentPrice;
                await _cacheService.SetPriceAsync(AssetId, asset.CurrentPrice);
            }
            else
            {
                CurrentPrice = cachedPrice.Value;
            }
            return CurrentPrice;
        }

        private async Task<decimal> GetUpdatedBalanceUser(decimal balance, decimal currentPrice, decimal quantity, AssetTransactionType type)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantidade deve ser maior que zero.");

            if (currentPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(currentPrice), "Preço atual deve ser maior que zero.");

            var totalPrice = currentPrice * quantity;

            return type switch
            {
                AssetTransactionType.Buy when balance < totalPrice =>
                    throw new InvalidOperationException("Usuário sem saldo disponível para esta transação."),

                AssetTransactionType.Buy => balance - totalPrice,

                AssetTransactionType.Sell => balance + totalPrice,

                _ => throw new NotSupportedException($"Tipo de transação '{type}' não é suportado.")
            };
        }


        private async Task<AssetTransaction> CreateAssetTransaction(CreateAssetTransactionCommand request, decimal CurrentPrice)
        {
            var transaction = new AssetTransaction(
                                        request.UserId,
                                        request.AssetId,
                                        request.Type,
                                        request.Quantity,
                                        CurrentPrice
                                    );

            return await _assetTransactionRepository.CreateAsync(transaction);
        }

        private async Task KafkaProducerLog(AssetTransaction createdAssetTransacion)
        {
            var message = JsonSerializer.Serialize(new
            {
                createdAssetTransacion.Id,
                createdAssetTransacion.UserId,
                createdAssetTransacion.AssetId,
                createdAssetTransacion.Type,
                createdAssetTransacion.Quantity,
                createdAssetTransacion.PriceAtExecution,
                createdAssetTransacion.Timestamp
            });

            await _kafkaProducer.SendAsync("asset-transactions", message);
        }
    }
}
