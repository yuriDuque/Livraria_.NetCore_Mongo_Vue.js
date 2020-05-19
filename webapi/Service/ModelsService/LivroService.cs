using Domain.Models;
using RepositoryMongo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ModelsService
{
    public interface ILivroService
    {
        IList<Livro> GetAll();
        Task<Livro> GetByIdAsync(long id);
        Task SaveAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task DeleteAsync(long id);
        Task<IList<Livro>> GetByTituloAsync(string titulo);
        Task AlugarAsync(Livro livro);
        Task<IList<Livro>> GetLivrosAlugadosByCliente(long idCliente);
    }

    public class LivroService : ILivroService
    {
        private readonly IMongoRepository<Livro> _livroRepository;
        private readonly IClienteService _clienteService;

        public LivroService(IMongoRepository<Livro> livroRepository, IClienteService clienteService)
        {
            _livroRepository = livroRepository;
            _clienteService = clienteService;
        }

        public IList<Livro> GetAll()
        {
            return _livroRepository.GetAll().ToList();
        }

        public Task<Livro> GetByIdAsync(long id)
        {
            return _livroRepository.FindByIdAsync(id);
        }

        public async Task SaveAsync(Livro livro)
        {
            var livrosExistentes = await GetByTituloAsync(livro.Titulo);
            if (livrosExistentes != null && livrosExistentes.Any())
                throw new Exception("Já existe um livro cadastrado com esse titulo");

            livro.DataCadastro = DateTime.Now;

            _ = _livroRepository.SaveAsync(livro);
        }

        public async Task UpdateAsync(Livro livro)
        {
            var livroBase = await GetByIdAsync(livro.Id);
            if (livroBase == null)
                throw new Exception("Não é possivel atualizar o livro, o livro não está cadastrado");

            var livrosExistentes = await GetByTituloAsync(livro.Titulo);
            if (livrosExistentes != null && livrosExistentes.Any())
                throw new Exception("Não é possivel atualizar o livro, já existe um livro cadastrado com esse titulo");

            if (livro.DataCadastro == new DateTime())
                livro.DataCadastro = livroBase.DataCadastro;

            _ = _livroRepository.UpdateAsync(livro);
        }

        public async Task DeleteAsync(long id)
        {
            var livroBase = await GetByIdAsync(id);
            if (livroBase == null)
                throw new Exception("Não é possivel deletar o livro, o livro não está cadastrado");

            _ = _livroRepository.DeleteByIdAsync(id);
        }

        public async Task<IList<Livro>> GetByTituloAsync(string titulo)
        {
            if (!string.IsNullOrEmpty(titulo))
                return await _livroRepository.FilterByAsync(x => x.Titulo.Equals(titulo));

            return null;
        }

        public async Task AlugarAsync(Livro livro)
        {
            var livroBase = await GetByIdAsync(livro.Id);
            if (livroBase == null)
                throw new Exception("Não é possivel alugar o livro, o livro não está cadastrado");

            if (livroBase.Alugado)
                throw new Exception("Não é possivel alugar o livro, o livro está alugado");

            var cliente = await _clienteService.GetByIdAsync((long) livro.IdCliente);
            if (cliente == null)
                throw new Exception("Não é possivel alugar o livro, o cliente não está cadastrado");

            livroBase.Alugado = true;
            livroBase.Cliente = cliente;
            livroBase.IdCliente = cliente.Id;

            _ = _livroRepository.UpdateAsync(livroBase);
        }

        public async Task<IList<Livro>> GetLivrosAlugadosByCliente(long idCliente)
        {
            return await _livroRepository.FilterByAsync(x => x.IdCliente == idCliente && x.Alugado == true);
        }
    }
}
