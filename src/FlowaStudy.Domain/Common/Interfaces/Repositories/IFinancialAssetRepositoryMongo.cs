using FlowaStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Domain.Common.Interfaces.Repositories
{
    public interface IFinancialAssetRepositoryMongo
    {
        Task InsertMongoDbAsync(FinancialAssetMongo asset);
        Task<List<FinancialAssetMongo>> GetAllMongoDbAsync();

    }
}
