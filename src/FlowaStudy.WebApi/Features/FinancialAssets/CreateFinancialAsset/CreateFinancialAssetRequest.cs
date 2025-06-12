namespace FlowaStudy.WebApi.Features.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetRequest
    {
        public string Name { get;  set; } = string.Empty;
        public decimal Value { get;  set; }
        public DateTime AcquisitionDate { get;  set; }
    }
}
