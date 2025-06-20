using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Application.Assets.CreateAsset
{
    public class CreateAssetCommand : IRequest<CreateAssetResult>
    {
        public string Symbol { get; set; } // Ex: "AAPL", "BTC"
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public CreateAssetCommand(string symbol, string name, decimal currentPrice)
        {
            Symbol = symbol;
            Name = name;
            CurrentPrice = currentPrice;
        }
    }
}
