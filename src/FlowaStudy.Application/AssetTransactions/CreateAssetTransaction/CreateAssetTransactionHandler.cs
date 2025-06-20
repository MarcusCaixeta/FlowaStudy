using AutoMapper;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using MediatR;

namespace FlowaStudy.Application.AssetTransactions.CreateAssetTransaction
{
    public class CreateAssetTransactionHandler : IRequestHandler<CreateAssetTransactionCommand, CreateAssetTransactionResult>
    {
        private readonly IMapper _mapper;
        private readonly IAssetTransactionRepository _assetTransactionRepository;

        public CreateAssetTransactionHandler(IMapper mapper, IAssetTransactionRepository assetTransactionRepository)
        {
            _mapper = mapper;
            _assetTransactionRepository = assetTransactionRepository;
        }
        public async Task<CreateAssetTransactionResult> Handle(CreateAssetTransactionCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<CreateAssetTransactionCommand>(command);
            var entity = new AssetTransaction(request.UserId, request.AssetId, request.Type, request.Quantity, request.PriceAtExecution);

            var createdAssetTransacion = await _assetTransactionRepository.CreateAsync(entity, cancellationToken);
            var result = _mapper.Map<CreateAssetTransactionResult>(createdAssetTransacion);

            return result;
        }
    }
}
