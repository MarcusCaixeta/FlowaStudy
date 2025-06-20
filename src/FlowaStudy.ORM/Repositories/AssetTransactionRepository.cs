using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using FlowaStudy.ORM.Contexts;

namespace FlowaStudy.ORM.Repositories 
{
    public class AssetTransactionRepository : IAssetTransactionRepository
    {
        private readonly EfContext _context;
        public AssetTransactionRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<AssetTransaction> CreateAsync(AssetTransaction assetTransaction, CancellationToken cancellationToken = default)
        {
            await _context.AssetTransaction.AddAsync(assetTransaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return assetTransaction;
        }
    }
}
