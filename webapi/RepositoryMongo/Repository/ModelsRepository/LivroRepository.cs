using Domain.Models;

namespace RepositoryMongo.Repository.ModelsRepository
{
    public class LivroRepository : MongoRepository<Livro>
    {
        public LivroRepository(MongoContext context) : base(context)
        {
        }

    }
}
