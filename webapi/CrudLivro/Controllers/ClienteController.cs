﻿using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ModelsService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrudLivro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILivroService _livroService;

        public ClienteController(IClienteService clienteService, ILivroService livroService)
        {
            _clienteService = clienteService;
            _livroService = livroService;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                var livro = await _clienteService.GetByIdAsync(id);

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
        public async Task<ActionResult> Save([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                await _clienteService.SaveAsync(cliente);

                return StatusCode(201, "Cliente adicionado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao salvar, " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] Cliente cliente, long id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors));

                var clienteBase = await _clienteService.GetByIdAsync(id);

                if (clienteBase == null)
                    return NotFound("Cliente não encontrado");

                await _clienteService.UpdateAsync(cliente);

                return Ok("Cliente atualizado com sucesso");
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
                var livro = await _clienteService.GetByIdAsync(id);

                if (livro == null)
                    return NotFound("Cliente não encontrado");

                await _clienteService.DeleteAsync(id);

                return StatusCode(201, "Cliente deletado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar, " + ex.Message);
            }
        }

        [HttpGet("livros-alugados/{id}")]
        public async Task<ActionResult> Livros(long id)
        {
            try
            {
                return Ok(await _livroService.GetLivrosAlugadosByCliente(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar, " + ex.Message);
            }
        }
    }
}