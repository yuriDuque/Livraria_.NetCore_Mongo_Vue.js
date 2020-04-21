using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Utils
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        long Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
