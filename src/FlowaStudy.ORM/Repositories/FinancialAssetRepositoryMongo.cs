using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.ORM.Repositories
{
    public class FinancialAssetRepositoryMongo : IFinancialAssetRepositoryMongo
    {
        private readonly IMongoCollection<FinancialAssetMongo> _collection;

        public FinancialAssetRepositoryMongo(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<FinancialAssetMongo>("FinancialAssets");
        }      

        public async Task InsertMongoDbAsync(FinancialAssetMongo asset)
        {
            await _collection.InsertOneAsync(asset);
        }

        public async Task<List<FinancialAssetMongo>> GetAllMongoDbAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
    }
}
