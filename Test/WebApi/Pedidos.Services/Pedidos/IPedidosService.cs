using Pedidos.Core;
using Pedidos.Core.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Pedidos
{
    public interface IPedidosService
    {
        bool InsertarPedido(Pedido pedido, List<DetallePedido> detallesPedido);
        bool InsertarPedido(Pedido pedido);
        ICollection<Pedido> GetPedidos(DateTime fechaDesde, DateTime fechaHasta, Enumeraciones.OrderQuery order = Enumeraciones.OrderQuery.ASC, int idSucursal=0);
        Pedido GetPedido(int idPedido, int idSucursal);
        bool EliminarPedido(int idPedido);
        Pedido GetPedidoCompleto(int idPedido, int idSucursal);
    }
}
