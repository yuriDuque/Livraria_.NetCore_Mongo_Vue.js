using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ModelsService;
using System;
using System.Linq;

namespace CrudLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_clienteService.GetAll());
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
                var livro = _clienteService.GetById(id);

                if (livro == null)
                    return NotFound("Cliente não encontrado");

                return Ok(livro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao buscar, " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Save([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                _clienteService.Save(cliente);

                return StatusCode(201, "Cliente adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar, " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Cliente cliente, long id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                var clienteBase = _clienteService.GetById(id);

                if (clienteBase == null)
                    return NotFound("Cliente não encontrado");

                _clienteService.Update(cliente);

                return Ok("Cliente atualizado com sucesso");
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
                var livro = _clienteService.GetById(id);

                if (livro == null)
                    return NotFound("Cliente não encontrado");

                _clienteService.Delete(id);

                return StatusCode(201, "Cliente deletado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar, " + ex.Message);
            }
        }
    }
}