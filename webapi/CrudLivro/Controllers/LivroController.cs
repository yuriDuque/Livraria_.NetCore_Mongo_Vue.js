using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryMongo.Repository;
using Service.ModelsService;
using System;

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

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_livroService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar, " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save([FromBody] Livro livro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro ao montar o objeto");

                _livroService.Save(livro);

                return StatusCode(201, "Infectado adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar, " + ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Update([FromBody] Livro livro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro ao montar o objeto");

                _livroService.Update(livro);

                return StatusCode(201, "Infectado adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar, " + ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            try
            {
                var livro = _livroService.GetById(id);

                if (livro == null)
                    return BadRequest("Livro não encontrado");

                _livroService.Delete(id);

                return StatusCode(201, "Livro deletado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar, " + ex.Message);
            }
        }
    }
}