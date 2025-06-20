using FlowaStudy.Domain.Entities;

namespace FlowaStudy.Domain.Common.Interfaces.Repositories
{
    public interface IAssetTransactionRepository
    {
        Task<AssetTransaction> CreateAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default);

    }
}
