using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using static Pedidos.Core.Enumeraciones.Enumeraciones;

namespace Pedidos.Repository.DetallesPedido
{
    public class DetallesPedidoRepository : BaseRepository, IDetallesPedidoRepository
    {
        public DetallesPedidoRepository(IConfiguration configuration) : base(configuration)
        { }

        //TODO: VER SI SE PUEDE UNIFICAR CON "GetAllDetallesPedidoFull", analizar donde se usa
        public ICollection<DetallePedido> GetAllDetallesPedido(int idPedido)
        {
            var sql = "select " +
                    "dp.nombreProducto, " +
                    "dp.cantidadProductoDetallePedido, " +
                    "dp.precioProducto, " +
                    "dp.observacionDetallePedido " +
                "from detallePedido dp " +
                "where dp.idPedido = @idPedido";
            var parameters = new Dictionary<string, object>()
            {
                {"idPedido", idPedido }
            };

            return GetListOf<DetallePedido>(sql, parameters);
        }

        /// <summary>
        /// Obtiene todos los datos del detalle de pedido, segun el Id de pedido que se pase por parametro
        /// </summary>
        /// <param name="idPedido">ID de pedido </param>
        /// <returns>DataTable</returns>
        public ICollection<DetallePedido> GetAllDetallesPedidoFull(int idPedido)
        {
            var sql = @"select 
                            dp.idDetallePedido,
                            dp.idPedido,
                            dp.idProducto,
                            dp.nombreProducto, 
                            dp.cantidadProductoDetallePedido, 
                            dp.precioProducto, 
                            dp.observacionDetallePedido 
                        from detallePedido dp 
                        where dp.idPedido = @idPedido";

            var parameters = new Dictionary<string, object>()
            {
                {"idPedido", idPedido }
            };

            return GetListOf<DetallePedido>(sql, parameters);
        }

        public bool EliminarDetallesDePedidoPorIPedido(int idPedido)
        {
            var sql = "Update DetallePedido set activo = 0 where idPedido = @idPedido";

            var parameters = new Dictionary<string, object>()
            {
                {"idPedido", idPedido }
            };

            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool InsertarDetallePedido(DetallePedido detallePedido, long idPedido, SqlTransaction transaction)
        {
            try
            {
                var sql = @"insert into DetallePedido (idPedido, idProducto, nombreProducto, cantidadProductoDetallePedido, precioProducto, 
                                observacionDetallePedido, idTipoDetallePedido)
                            values (@idPedido, @idProducto, @nombreProducto, @cantidadProductoDetallePedido, 
                                @precioProducto, @observacionDetallePedido, 
                                (select id from TipoDetallePedido where nombre = @nombreTipoDetallePedido))";

                var parameters = new Dictionary<string, object>()
                        {
                            {"idPedido", idPedido},
                            {"idProducto", detallePedido.IdProducto},
                            {"cantidadProductoDetallePedido", detallePedido.CantidadProductoDetallePedido},
                            {"observacionDetallePedido", detallePedido.ObservacionDetallePedido},                            
                            {"nombreProducto", detallePedido.Nombre},
                            {"precioProducto", detallePedido.PrecioDetalleProducto},
                            {"nombreTipoDetallePedido", ((TipoDetallePedido)detallePedido.TipoDetallePedido).ToString()}
                        };


                ExecuteNonQuery(sql, parameters, false, transaction.Connection, transaction);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<DetallePedido> GetCantidadesVendidas(DateTime fechaDesde, DateTime fechaHasta, int idSucursal)
        {
            var sql = "Pedidos_getSumaProductosPorSucursal";
            var parameters = new Dictionary<string, object>()
            {
                {"fechaDesde", fechaDesde },
                {"fechaHasta", fechaHasta },
                {"idSucursal", idSucursal }
            };            
            return GetListOf<DetallePedido>(sql, parameters, true);
        }

        public ICollection<DetallePedido> GetCantidadesVendidas(DateTime fechaDesde, DateTime fechaHasta)
        {
            var sql = "Pedidos_getSumaProductosTodasSucursales";
            var parameters = new Dictionary<string, object>()
            {
                {"fechaDesde", fechaDesde },
                {"fechaHasta", fechaHasta }
            };

            return GetListOf<DetallePedido>(sql, parameters, true);
        }
    }
}
