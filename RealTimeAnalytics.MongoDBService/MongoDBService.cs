using MongoDB.Bson;
using MongoDB.Driver;
using RealTimeAnalytics.Shared;

namespace RealTimeAnalytics.MongoDBService
{
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public MongoDBService(string? connectionString, string? databaseName, string? collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<BsonDocument>(collectionName);
        }

        public async Task SaveDataAsync(string data)
        {
            var document = new BsonDocument
            {
                { "value", data }
            };
            await _collection.InsertOneAsync(document);
        }
    }
}
