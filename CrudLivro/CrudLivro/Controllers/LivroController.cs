using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryMongo.Repository;
using System.Linq;

namespace CrudLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMongoRepository<Livro> _livroRepository;

        public LivroController(IMongoRepository<Livro> livroRepository)
        {
            _livroRepository = livroRepository;
        }

        [HttpPost]
        public ActionResult SalvarInfectado()
        {
            var livro = new Livro();
            livro.Titulo = "Teste";
            livro.Alugado = true;

            _livroRepository.InsertOne(livro);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var ivros = _livroRepository.GetAll().ToList();

            return Ok(ivros);
        }
    }
}