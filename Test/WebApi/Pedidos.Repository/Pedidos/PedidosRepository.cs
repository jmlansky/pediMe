using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Core.Enumeraciones;
using Pedidos.Repository.Comun;
using Pedidos.Repository.DetallesPedido;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Pedidos.Repository.Pedidos
{
    public class PedidosRepository : BaseRepository, IPedidosRepository
    {
        private readonly IDetallesPedidoRepository detallesPedidoRepository;
        public PedidosRepository(IConfiguration configuration, IDetallesPedidoRepository detallesPedidoRepository) : base(configuration)
        {
            this.detallesPedidoRepository = detallesPedidoRepository;
        }

        public bool InsertarPedido(Pedido pedido, List<DetallePedido> detallesPedido)
        {
            using (SqlTransaction tran = OpenConnectionWithTransaction())
            {
                try
                {
                    var idPedido = OnInsertarPedido(pedido, tran);
                    OnInsertarDetallesPedido(detallesPedido, tran, idPedido);

                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public bool InsertarPedido(Pedido pedido)
        {
            using (SqlTransaction tran = OpenConnectionWithTransaction())
            {
                try
                {
                    var idPedido = OnInsertarPedido(pedido, tran);
                    OnInsertarDetallesPedido(pedido.ListaDetalle.ToList(), tran, idPedido);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Devuelve todos los pedidos realizados en un intervalo de tiempo
        /// </summary>
        /// <param name="fechaDesde">Fecha desde </param>
        /// <param name="fechaHasta">Fecha hasta</param>
        /// <param name="order">OrderEnum Orden en que se desea recuperar los datos</param>
        public ICollection<Pedido> GetPedidos(DateTime fechaDesde, DateTime fechaHasta, Enumeraciones.OrderQuery order, int idSucursal)
        {
            //var sql = "Select suc.direccion as direccionSuc, ped.idPedido, ped.fechaPedido, ped.horaPedido, " +
            //                "ped.domicilioEntregaPedido, ped.montoTotalPedido, ped.nombreClienteMostrador from pedido ped, Sucursal suc " +
            //                "where fechaPedido between @fechaDesde and @fechaHasta activa = 1 and " +
            //                "activaAdmin = 1 order by ped.horaPedido " + order;

            var sql = @"Select ped.idPedido, ped.fechaPedido, 
                        ped.domicilioEntregaPedido, ped.montoTotalPedido, ped.nombreCliente,
                        ped.observacionPedido
                        from pedido ped
                        where fechaPedido between @fechaDesde and @fechaHasta 
                        order by ped.fechaPedido " + order;

            var parameters = new Dictionary<string, object>()
            {
                { "fechaDesde", fechaDesde},
                { "fechaHasta", fechaHasta},
                { "order", order}
            };
            return GetListOf<Pedido>(sql, parameters);

            //        if (pIdSucursal == 0)
            //            com = new SqlCommand("Select suc.direccion as direccionSuc, ped.idPedido, ped.fechaPedido, ped.horaPedido, " +
            //                "ped.domicilioEntregaPedido, ped.montoTotalPedido, ped.nombreClienteMostrador from pedido ped, Sucursal suc " +
            //                "where fechaPedido between @fechaDesde and @fechaHasta and suc.idSucursal = ped.idSucursal and activa = 1 and " +
            //                "activaAdmin = 1 order by ped.horaPedido " + order, con.conection);
            //        else
            //        {
            //            string oConsultaAdicional = "";
            //            if (envio == "Mesa")
            //                oConsultaAdicional = "and ped.domicilioEntregaPedido like @envio ";
            //            else
            //                oConsultaAdicional = "and ped.domicilioEntregaPedido not like 'Mesa%' ";

            //            string oConsultaOrder = "order by ped.horaPedido " + order;

            //            com = new SqlCommand("Select suc.direccion as direccionSuc, ped.idPedido, ped.fechaPedido, ped.horaPedido, " +
            //                "ped.domicilioEntregaPedido, ped.montoTotalPedido, ped.nombreClienteMostrador from pedido ped, Sucursal suc " +
            //                "where fechaPedido between @fechaDesde and @fechaHasta and suc.idSucursal = ped.idSucursal and " +
            //                "suc.idSucursal = @idSucursal " + oConsultaAdicional + oConsultaOrder, con.conection);
            //            com.Parameters.AddWithValue("@idSucursal", idSucursal);
            //            com.Parameters.AddWithValue("@envio", envio + "%");
            //        }
        }

        /// <summary>
        /// Metodo que busca un pedido segun el Id pasado por parametro, puede devolver null en caso de que el id
        /// no se encuentre en la DB
        /// </summary>
        /// <param name="pIdPedido">ID pedido a buscar</param>
        /// <returns>Pedido encontrado, puede devolver null</returns>
        public Pedido GetPedido(int idPedido, int idSucursal)
        {
            var sql = @"Select ped.domicilioEntregaPedido, ped.fechaPedido, ped.horaPedido, ped.tiempoDemora, 
		                    ped.montoTotalPedido, ped.montoAbono, (ped.montoAbono - ped.montoTotalPedido) as vuelto,
		                    del.precioDelivery, cli.nombreCliente, cli.telefonoCliente, ped.observacionPedido, ped.idSucursal 
                        from pedido ped 
                        where ped.idPedido = @idPedido
                        and ped.idSucursal = @idSucursal";

            var parameters = new Dictionary<string, object>()
            {
                { "idPedido", idPedido },
                { "idSucursal", idSucursal }
            };
            return Get<Pedido>(sql, parameters);
        }

        /// <summary>
        /// Elimina el pedido pasado por parametro
        /// </summary>
        /// <param name="idPedido"> ID del pedido a eliminar</param>
        /// <param name="eliminarDetalles">Flag que indica si se debe eliminar los detalles de pedidos tambien</param>
        /// <returns> Flag que indica si se pudo realizar la operacion solicitada</returns>
        public bool EliminarPedido(int idPedido)
        {
            var sql = "Update Pedido set activo = 0 where idPedido = @idPedido; Update detallePedido set activo = 0 where idPedido = @idPedido";
            var parameters = new Dictionary<string, object>()
            {
                { "idPedido", idPedido}
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public Pedido GetPedidoCompleto(int idPedido, int idSucursal)
        {
            var pedido = GetPedido(idPedido, idSucursal);
            var detallesDePedido = detallesPedidoRepository.GetAllDetallesPedido(idPedido);

            pedido.ListaDetalle.ToList().AddRange(detallesDePedido.ToList());

            return pedido;
        }

        private void OnInsertarDetallesPedido(List<DetallePedido> detallesPedido, SqlTransaction transaction, long idPedido)
        {
            foreach (var detP in detallesPedido)
            {
                detallesPedidoRepository.InsertarDetallePedido(detP, idPedido, transaction);
            }
        }

        private static long OnInsertarPedido(Pedido pedido, SqlTransaction tran)
        {
            var sql = @"insert into Pedido 
                            (idCliente, montoTotalPedido, fechaPedido, observacionPedido, domicilioEntregaPedido, montoAbono, tiempoDemora, descuento, nombreCliente, idSucursal, activo)
                            values (@idCliente, @montoTotalPedido, @fechaPedido, @observacionPedido, @domicilioEntregaPedido, @montoAbono, @tiempoDemora, @descuento, @nombreCliente, @idSucursal, 1);
                        SELECT SCOPE_IDENTITY(); ";
            var parameters = new Dictionary<string, object>()
                    {
                        {"idCliente", pedido.Cliente.Id},
                        {"montoTotalPedido", pedido.MontoTotalPedido},
                        {"fechaPedido", Convert.ToDateTime(pedido.FechaPedido)},                        
                        {"observacionPedido", pedido.Pago.Notas},
                        {"domicilioEntregaPedido", pedido.Cliente.Domicilio},
                        {"montoAbono", pedido.Pago.Abono},
                        {"tiempoDemora", pedido.Pago.Demora},
                        {"descuento", pedido.Descuento},
                        {"nombreCliente", pedido.Cliente.Nombre},
                        {"idSucursal", pedido.IdSucursal}
                    };

            return Convert.ToInt64(ExecuteScalarQuery(sql, parameters, isStoredProcedure: false, connection: tran.Connection, transaction: tran));
        }

        #region TODO definir si ´Precio del delivery' va en este repositorio o no
        public bool InsertarPrecioDelivery(decimal precio, int idSucursal)
        {
            return false;
            //Conexion con = new Conexion();
            //if (con.conection.State == ConnectionState.Open)
            //{
            //    con.conection.Close();
            //}

            //con.conection.Open();
            //SqlTransaction tran = con.conection.BeginTransaction();
            //try
            //{
            //    SqlCommand com = new SqlCommand("insert into Delivery (precioDelivery, idSucursal) values (@precioDelivery, @idSucursal)", con.conection);
            //    com.Parameters.AddWithValue("@precioDelivery", precio);
            //    com.Parameters.AddWithValue("@idSucursal", idSucursal);
            //    com.Transaction = tran;
            //    com.ExecuteNonQuery();
            //    tran.Commit();
            //    respuesta = true;
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //    tran.Rollback();
            //}

            //con.conection.Close();
            //return respuesta;
        }

        public bool ActualizarPrecioDelivery(decimal precio, int idSucursal)
        {
            return false;
            //bool respuesta = false;
            //Conexion con = new Conexion();
            //if (con.conection.State == ConnectionState.Open)
            //{
            //    con.conection.Close();
            //}

            //con.conection.Open();
            //SqlTransaction tran = con.conection.BeginTransaction();
            //try
            //{
            //    SqlCommand com = new SqlCommand("Update Delivery set precioDelivery = @precioDelivery where idSucursal = @idSucursal", con.conection);
            //    com.Parameters.AddWithValue("@precioDelivery", precio);
            //    com.Parameters.AddWithValue("@idSucursal", idSucursal);
            //    com.Transaction = tran;
            //    com.ExecuteNonQuery();
            //    tran.Commit();
            //    respuesta = true;
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //    tran.Rollback();
            //}

            //con.conection.Close();
            //return respuesta;
        }

        public decimal GetPrecioDelivery(int idSucursal)
        {
            return -1;
            //Conexion con = new Conexion();
            //if (con.conection.State == ConnectionState.Open)
            //    con.conection.Close();

            //con.conection.Open();
            //SqlCommand com = new SqlCommand("Select precioDelivery from Delivery where idSucursal = @idSucursal", con.conection);
            //try
            //{
            //    com.Parameters.AddWithValue("@idSucursal", idSucursal);
            //    precio = Convert.ToDecimal(com.ExecuteScalar().ToString());
            //}
            //catch (Exception ex)
            //{
            //    precio = -1;
            //    ex.Message.ToString();
            //}

            //con.conection.Close();
            //return precio;
        }
        #endregion
    }
}
