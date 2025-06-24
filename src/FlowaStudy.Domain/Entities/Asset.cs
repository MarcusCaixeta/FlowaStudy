
using FlowaStudy.Domain.Common;

namespace FlowaStudy.Domain.Entities
{
    public class Asset : BaseEntity
    {
        public Asset(string symbol, string name, decimal currentPrice)
        {
            Symbol = symbol;
            Name = name;
            CurrentPrice = currentPrice;
        }

        public string Symbol { get; set; } // Ex: "AAPL", "BTC"
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    }
}
