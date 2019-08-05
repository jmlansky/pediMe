using Pedidos.Core;
using Pedidos.Core.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Pedidos
{
    public interface IPedidosRepository
    {
        bool InsertarPedido(Pedido pedido, List<DetallePedido> detallesPedido);
        bool InsertarPedido(Pedido pedido);
        ICollection<Pedido> GetPedidos(DateTime fechaDesde, DateTime fechaHasta, Enumeraciones.OrderQuery order, int idSucursal);
        Pedido GetPedido(int idPedido, int idSucursal);
        bool EliminarPedido(int idPedido);        
        Pedido GetPedidoCompleto(int idPedido, int idSucursal);
    }
}
