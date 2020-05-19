using Domain.Models;
using RepositoryMongo.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Service.ModelsService
{
    public interface IClienteService
    {
        IList<Cliente> GetAll();
        Cliente GetById(long id);
        void Save(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(long id);
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

        public Cliente GetById(long id)
        {
            return _clienteRepository.FindById(id);
        }

        public void Save(Cliente cliente)
        {
            _clienteRepository.Save(cliente);
        }

        public void Update(Cliente cliente)
        {
            _clienteRepository.Update(cliente);
        }

        public void Delete(long id)
        {
            _clienteRepository.DeleteById(id);
        }
    }
}
