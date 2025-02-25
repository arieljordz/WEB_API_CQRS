using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WEB_API_CQRS.src.Infrastructure.Persistence
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<DatabaseSettings> settings)
        {
            if (settings?.Value?.ConnectionString == null)
                throw new ArgumentNullException(nameof(settings.Value.ConnectionString), "MongoDB connection string is missing.");

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
