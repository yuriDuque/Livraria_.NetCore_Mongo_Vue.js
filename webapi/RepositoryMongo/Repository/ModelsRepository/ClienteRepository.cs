using Domain.Models;

namespace RepositoryMongo.Repository.ModelsRepository
{
    public class ClienteRepository : MongoRepository<Cliente>
    {
        public ClienteRepository(MongoContext context) : base(context)
        {
        }

    }
}
