using FluentValidation;

namespace FlowaStudy.WebApi.Features.Assets.CreateAsset
{
    public class CreateAssetRequestValidator : AbstractValidator<CreateAssetRequest>
    {
        public  CreateAssetRequestValidator()
        {
            RuleFor(asset => asset.Symbol).NotEmpty();
        }
    }
}
