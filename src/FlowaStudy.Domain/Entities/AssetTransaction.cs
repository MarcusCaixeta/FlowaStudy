using FlowaStudy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Domain.Entities
{
    public class AssetTransaction : BaseEntity
    {
        public AssetTransaction(Guid userId, Guid assetId, AssetTransactionType type, decimal quantity, decimal priceAtExecution)
        {
            UserId = userId;
            AssetId = assetId;
            Type = type;
            Quantity = quantity;
            PriceAtExecution = priceAtExecution;
        }

        public Guid UserId { get; set; }

        public Guid AssetId { get; set; }

        public AssetTransactionType Type { get; set; } // Buy or Sell
        public decimal Quantity { get; set; }
        public decimal PriceAtExecution { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public enum AssetTransactionType
    {
        Buy,
        Sell
    }
}
