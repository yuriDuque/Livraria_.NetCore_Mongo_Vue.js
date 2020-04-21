using Domain.Models;
using MongoDB.Bson.Serialization;

namespace RepositoryMongo.Map
{
    public class ClienteMap
    {
        public ClienteMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Cliente)))
            {
                BsonClassMap.RegisterClassMap<Cliente>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
