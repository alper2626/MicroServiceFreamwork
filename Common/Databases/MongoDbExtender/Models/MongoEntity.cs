using EntityBase.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbExtender.Converter;
using System.Text.Json.Serialization;

namespace MongoDbExtender.Models
{
    public class MongoEntity : IEntity
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        [BsonElement(Order = 0)]
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 200)]
        public DateTime CreateTime { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 201)]
        public DateTime UpdateTime { get; set; }
    }

    public class RemovebleMongoEntity : MongoEntity, IRemovableEntity
    {
        public bool IsRemoved { get; set; }
    }
}
