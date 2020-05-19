using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Utils
{
    public abstract class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64)]
        public long Id { get; set; }
    }
}
