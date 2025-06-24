using FlowaStudy.Domain.Entities;

namespace FlowaStudy.WebApi.Features.AssetTransactions.CreateAssetTransaction
{
    public class CreateAssetTransactionRequest
    {
        public Guid UserId { get; set; }

        public Guid AssetId { get; set; }

        public AssetTransactionType Type { get; set; } // Buy or Sell
        public decimal Quantity { get; set; }
    }
}
