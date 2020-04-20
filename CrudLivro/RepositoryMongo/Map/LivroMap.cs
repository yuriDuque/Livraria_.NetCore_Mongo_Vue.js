using Domain;
using MongoDB.Bson.Serialization;

namespace RepositoryMongo.Map
{
    public class LivroMap
    {
        public LivroMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Livro)))
            {
                BsonClassMap.RegisterClassMap<Livro>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
