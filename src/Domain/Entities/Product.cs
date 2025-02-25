using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WEB_API_CQRS.src.Domain.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }
    }
}
