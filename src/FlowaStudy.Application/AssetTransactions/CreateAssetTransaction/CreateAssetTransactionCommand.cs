using FlowaStudy.Domain.Entities;
using MediatR;

namespace FlowaStudy.Application.AssetTransactions.CreateAssetTransaction
{
    public class CreateAssetTransactionCommand : IRequest<CreateAssetTransactionResult>
    {    
        public Guid UserId { get; set; }

        public Guid AssetId { get; set; }

        public AssetTransactionType Type { get; set; } // Buy or Sell
        public decimal Quantity { get; set; }

        public CreateAssetTransactionCommand(Guid userId, Guid assetId, AssetTransactionType type, decimal quantity)
        {
            UserId = userId;
            AssetId = assetId;
            Type = type;
            Quantity = quantity;
        }
    }
}
