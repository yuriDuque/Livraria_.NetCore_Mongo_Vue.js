using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.ModelsService;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                var livro = await _livroService.GetByIdAsync(id);

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
        public async Task<ActionResult> Save([FromBody] Livro livro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                await _livroService.SaveAsync(livro);

                return StatusCode(201, "Livro adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar, " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] Livro livro, long id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                var livroBase = await _livroService.GetByIdAsync(id);

                if (livroBase == null)
                    return NotFound("Livro não encontrado");

                await _livroService.UpdateAsync(livro);

                return Ok("Livro atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar, " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _livroService.DeleteAsync(id);

                return StatusCode(200, "Livro deletado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar, " + ex.Message);
            }
        }

        [HttpPost("alugar")]
        public ActionResult Alugar([FromBody] Livro livro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                _livroService.AlugarAsync(livro);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar, " + ex.Message);
            }
        }
    }
}