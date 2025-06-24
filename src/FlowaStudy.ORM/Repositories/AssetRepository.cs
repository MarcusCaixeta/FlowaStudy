using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using FlowaStudy.ORM.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FlowaStudy.ORM.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly EfContext _context;
        public AssetRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<Asset> CreateAsync(Asset asset, CancellationToken cancellationToken = default)
        {
            await _context.Asset.AddAsync(asset, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return asset;
        }

        public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Asset.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}
