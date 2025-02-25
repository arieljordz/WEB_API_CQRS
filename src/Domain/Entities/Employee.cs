using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WEB_API_CQRS.src.Domain.Entities
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [BsonElement("MiddleName")]
        public string MiddleName { get; set; } = string.Empty;

        [BsonElement("LastName")]
        public string LastName { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;
    }
}
