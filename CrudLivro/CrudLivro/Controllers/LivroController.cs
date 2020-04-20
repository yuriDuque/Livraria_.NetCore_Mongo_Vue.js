using System.Linq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RepositoryMongo;

namespace CrudLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        MongoContext _mongoDB;
        IMongoCollection<Livro> _livroCollection;

        public LivroController(MongoContext mongoDB)
        {
            _mongoDB = mongoDB;
            _livroCollection = _mongoDB.DB.GetCollection<Livro>(typeof(Livro).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] Livro livro)
        {
            _livroCollection.InsertOne(livro);
            
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _livroCollection.Find(Builders<Livro>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }
    }
}