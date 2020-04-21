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

        public IList<Livro> GetAll()
        {
            return _livroRepository.GetAll().ToList();
        }

        public Livro GetById(long id)
        {
            return _livroRepository.FindById(id);
        }

        public void Save(Livro livro)
        {
            _livroRepository.Save(livro);
        }

        public void Update(Livro livro)
        {
            _livroRepository.Update(livro);
        }

        public void Delete(long id)
        {
            _livroRepository.DeleteById(id);
        }
    }
}
