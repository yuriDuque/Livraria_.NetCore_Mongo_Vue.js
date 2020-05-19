using Domain.Models;
using RepositoryMongo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ModelsService
{
    public interface IClienteService
    {
        IList<Cliente> GetAll();
        Task<Cliente> GetByIdAsync(long idCliente);
        Task SaveAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(long id);        
    }

    public class ClienteService : IClienteService
    {
        private readonly IMongoRepository<Cliente> _clienteRepository;

        public ClienteService(IMongoRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IList<Cliente> GetAll()
        {
            return _clienteRepository.GetAll().ToList();
        }

        public Task<Cliente> GetByIdAsync(long idCliente)
        {
            return _clienteRepository.FindByIdAsync(idCliente);
        }

        public async Task<Cliente> GetByCPF(string CPF)
        {
            return (await _clienteRepository.FilterByAsync(x => x.CPF.Equals(CPF))).FirstOrDefault();            
        }

        public async Task SaveAsync(Cliente cliente)
        {
            var clienteBase = await GetByCPF(cliente.CPF);

            if (clienteBase != null)
                throw new Exception("Não é possivel salvar o cliente, o CPF já está cadastrado");

            cliente.DataCadastro = DateTime.Now;

            await _clienteRepository.SaveAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            var clienteBase = await GetByIdAsync(cliente.Id);
            var clienteCPF = await GetByCPF(cliente.CPF);

            if (clienteBase == null)
                throw new Exception("Não é possivel atualizar o cliente, o cliente não está cadastrado");

            if (clienteCPF != null && clienteCPF.Id != cliente.Id)
                throw new Exception("Não é possivel alterar o cliente, o CPF já está cadastrado");

            if (cliente.DataCadastro == new DateTime())
                cliente.DataCadastro = clienteBase.DataCadastro;

            _clienteRepository.Update(cliente);
        }

        public async Task DeleteAsync(long id)
        {
            var clienteBase = await GetByIdAsync(id);

            if (clienteBase == null)
                throw new Exception("Não é possivel atualizar o cliente, o cliente não está cadastrado");

            _clienteRepository.DeleteById(id);
        }
    }
}
