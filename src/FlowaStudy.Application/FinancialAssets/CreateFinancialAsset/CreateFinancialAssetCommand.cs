using FlowaStudy.Common.Validation;
using MediatR;

namespace FlowaStudy.Application.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetCommand : IRequest<CreateFinancialAssetResult>
    {    
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public CreateFinancialAssetCommand(string name, decimal value, DateTime acquisitionDate)
        {
            Name = name;
            Value = value;
            AcquisitionDate = acquisitionDate;
        }
    }
}
