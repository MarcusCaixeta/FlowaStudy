using FluentValidation;

namespace FlowaStudy.WebApi.Features.AssetTransactions.CreateAssetTransaction
{
    public class CreateAssetTransactionRequestValidator : AbstractValidator<CreateAssetTransactionRequest>
    {
        public CreateAssetTransactionRequestValidator()
        {
            RuleFor(assetTransaction => assetTransaction.AssetId).NotEmpty();
        }
    }
}
