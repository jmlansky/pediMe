using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Repository.Clientes
{
    public interface IClientesRepository
    {
        bool InsertarCliente(Cliente cli);
        bool UpdateCliente(Cliente cli);
        Cliente GetCliente(string telefono);
        Cliente GetCliente(int idCliente);
        ICollection<Cliente> GetClientes();
        ICollection<Cliente> GetClientes(string telefono);
        int GetIdCliente(string telefono);        
    }
}
