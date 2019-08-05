using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Pedidos.Repository.DetallesPedido
{
    public interface IDetallesPedidoRepository
    {
        ICollection<DetallePedido> GetAllDetallesPedido(int idPedido);
        ICollection<DetallePedido> GetAllDetallesPedidoFull(int idPedido);
        bool EliminarDetallesDePedidoPorIPedido(int idPedido);
        bool InsertarDetallePedido(DetallePedido detallePedido, long idPedido, SqlTransaction transaction);
        ICollection<DetallePedido> GetCantidadesVendidas(DateTime fechaDesde, DateTime fechaHasta, int idSucursal);
        ICollection<DetallePedido> GetCantidadesVendidas(DateTime fechaDesde, DateTime fechaHasta);
    }
}
