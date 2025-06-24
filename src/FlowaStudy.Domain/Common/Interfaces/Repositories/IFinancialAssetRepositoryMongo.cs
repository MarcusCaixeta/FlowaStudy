using FlowaStudy.Domain.Entities;

namespace FlowaStudy.Domain.Common.Interfaces.Repositories
{
    public interface IFinancialAssetRepositoryMongo
    {
        Task InsertMongoDbAsync(FinancialAssetMongo asset);
        Task<List<FinancialAssetMongo>> GetAllMongoDbAsync();

    }
}
