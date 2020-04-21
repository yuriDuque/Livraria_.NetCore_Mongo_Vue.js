using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Utils
{
    public abstract class Document : IDocument
    {
        [BsonId]
        public long Id { get; set; }

        public DateTime CreatedAt { get; }
    }
}
