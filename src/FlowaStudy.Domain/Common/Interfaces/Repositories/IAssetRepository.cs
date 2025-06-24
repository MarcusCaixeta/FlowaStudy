using FlowaStudy.Domain.Entities;

namespace FlowaStudy.Domain.Common.Interfaces.Repositories
{
    public interface IAssetRepository
    {
        Task<Asset> CreateAsync(Asset asset, CancellationToken cancellationToken = default);
        Task<Asset?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    }
}
