namespace FlowaStudy.WebApi.Features.Assets.CreateAsset
{
    public class CreateAssetRequest
    {
        public string Symbol { get; set; } // Ex: "AAPL", "BTC"
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
