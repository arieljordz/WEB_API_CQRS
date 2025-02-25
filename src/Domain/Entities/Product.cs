using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WEB_API_CQRS.src.Domain.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}
