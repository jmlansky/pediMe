using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Clientes
{
    public interface IClientesService
    {
        bool InsertarCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        int GetIdCliente();
        Cliente GetCliente(int idCliente);
        Cliente GetCliente(string telefono);
        ICollection<Cliente> GetClientes(string telefono = "");
    }
}
