using FluentValidation;

namespace FlowaStudy.WebApi.Features.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetRequestValidator : AbstractValidator<CreateFinancialAssetRequest>
    {
        public CreateFinancialAssetRequestValidator()
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Value).NotEmpty();
            RuleFor(user => user.AcquisitionDate).NotEmpty();
        }
    }
}
