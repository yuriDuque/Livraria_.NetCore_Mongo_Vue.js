using Domain.Models;
using RepositoryMongo.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Service.ModelsService
{
    public class LivroService
    {
        private readonly IMongoRepository<Livro> _livroRepository;

        public LivroService(IMongoRepository<Livro> livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public void Save(Livro livro)
        {
            _livroRepository.InsertOne(livro);
        }

        public IList<Livro> GetAll()
        {
            return _livroRepository.GetAll().ToList();
        }
    }
}
