using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.ModelsService;
using System;
using System.Linq;

namespace CrudLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
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

        [HttpGet("{id}")]
        public ActionResult GetById(long id)
        {
            try
            {
                var livro = _livroService.GetById(id);

                if (livro == null)
                    return NotFound("Livro não encontrado");

                return Ok(livro);
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
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                _livroService.Save(livro);

                return StatusCode(201, "Livro adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar, " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Livro livro, long id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                var livroBase = _livroService.GetById(id);

                if (livroBase == null)
                    return NotFound("Livro não encontrado");

                _livroService.Update(livro);

                return Ok("Livro atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar, " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                var livro = _livroService.GetById(id);

                if (livro == null)
                    return NotFound("Livro não encontrado");

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