using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.DetallesPedido
{
    public interface IDetallesPedidoService
    {
        ICollection<DetallePedido> GetAllDetallesPedido(int idPedido);
        ICollection<DetallePedido> GetAllDetallesPedidoFull(int idPedido);
        bool EliminarDetallesDePedidoPorIPedido(int idPedido);
    }
}
