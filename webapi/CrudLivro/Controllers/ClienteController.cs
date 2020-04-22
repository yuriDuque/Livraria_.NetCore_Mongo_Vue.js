using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.ModelsService;
using System;

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

        [HttpPost]
        public ActionResult Save([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro ao montar o objeto");

                _clienteService.Save(cliente);

                return StatusCode(201, "Cliente adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar, " + ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Update([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro ao montar o objeto");

                _clienteService.Update(cliente);

                return StatusCode(201, "Cliente atualizado com sucesso");
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
                var cliente = _clienteService.GetById(id);

                if (cliente == null)
                    return BadRequest("Cliente não encontrado");

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