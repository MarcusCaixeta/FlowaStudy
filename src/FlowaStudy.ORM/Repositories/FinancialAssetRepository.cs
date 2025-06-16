using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using FlowaStudy.ORM.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.ORM.Repositories
{
    public class FinancialAssetRepository : IFinancialAssetRepository
    {
        private readonly EfContext _context;
        public FinancialAssetRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<FinancialAsset> CreateAsync(FinancialAsset financialAsset, CancellationToken cancellationToken = default)
        {
            await _context.FinancialAssets.AddAsync(financialAsset, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return financialAsset;
        }

        public async Task<List<FinancialAsset>?> GetAll(CancellationToken cancellationToken = default)
        {
            return (await _context.FinancialAssets.ToListAsync(cancellationToken)).Cast<FinancialAsset?>().ToList();
        }
    }
}
