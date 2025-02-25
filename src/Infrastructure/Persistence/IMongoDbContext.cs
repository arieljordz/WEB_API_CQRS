using MongoDB.Driver;

namespace WEB_API_CQRS.src.Infrastructure.Persistence
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
