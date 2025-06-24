
using FlowaStudy.Application.Messaging.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlowaStudy.ORM.Repositories.Mongo
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public LogRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<BsonDocument>("logs");
        }

        public async Task SaveAsync(string key, string json, CancellationToken cancellationToken = default)
        {
            var document = BsonDocument.Parse(json);
            document["logType"] = key;                 // Adiciona metadado do tipo
            document["loggedAt"] = DateTime.UtcNow;    // Timestamp automático

            await _collection.InsertOneAsync(document, options: null, cancellationToken); // ✅ evita warning
            Console.WriteLine($"✅ Documento inserido no MongoDB: {document["_id"]}");

            var exists = await _collection
    .Find(x => x["logType"] == "asset-transaction")
    .AnyAsync();

            Console.WriteLine($"Existe log? {exists}");
        }
    }
}
