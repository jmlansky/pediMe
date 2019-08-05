using System.Collections.Generic;
using Pedidos.Core;

namespace Pedidos.Services.OrigenesDePedido
{
    public interface IOrigenPedidoService
    {
        ICollection<OrigenPedido> GetOrigenesDePedido();
    }
}