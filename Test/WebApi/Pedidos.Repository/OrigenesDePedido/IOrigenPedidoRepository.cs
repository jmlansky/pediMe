using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;

namespace Pedidos.Repository.OrigenesDePedido
{
    public interface IOrigenPedidoRepository
    {
        ICollection<OrigenPedido> GetOrigenesDePedidos();
    }
}
