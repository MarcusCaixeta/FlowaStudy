using FlowaStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Domain.Common.Interfaces.Repositories
{
    public interface IFinancialAssetRepository
    {
        Task<FinancialAsset> CreateAsync(FinancialAsset financialAsset, CancellationToken cancellationToken = default);
        Task<List<FinancialAsset>?> GetAll(CancellationToken cancellationToken = default);
    }
}
