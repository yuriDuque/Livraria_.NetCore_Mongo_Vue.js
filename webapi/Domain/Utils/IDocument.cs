using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Utils
{
    public interface IDocument
    {
        long Id { get; set; }
    }
}
