using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryMongo.Repository;
using Service.ModelsService;

namespace CrudLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivroController(IMongoRepository<Livro> livroRepository)
        {
            _livroService = new LivroService(livroRepository);
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
                return BadRequest("Erro ao montar o objeto");

            _livroService.Save(livro);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            return Ok(_livroService.GetAll());
        }
    }
}