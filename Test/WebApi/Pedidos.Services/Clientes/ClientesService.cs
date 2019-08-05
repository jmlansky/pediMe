using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Repository.Clientes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Services.Clientes
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository clientesRepository;
        public ClientesService(IClientesRepository clientesRepository)
        {
            this.clientesRepository = clientesRepository;
        }

        public bool InsertarCliente(Cliente cliente)
        {
            try
            {
                clientesRepository.InsertarCliente((Cliente)cliente);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCliente(Cliente cliente)
        {
            try
            {                
                return clientesRepository.UpdateCliente(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetIdCliente()
        {
            string telefono = "111";
            return clientesRepository.GetIdCliente(telefono);
        }

        public Cliente GetCliente(int idCliente)
        {
            return clientesRepository.GetCliente(idCliente);
        }

        public Cliente GetCliente(string telefono)
        {
            return clientesRepository.GetCliente(telefono);
        }

        public ICollection<Cliente> GetClientes(string telefono = "")
        {
            if (string.IsNullOrEmpty(telefono))
                return clientesRepository.GetClientes();

            return clientesRepository.GetClientes(telefono);
        }
    }
}
