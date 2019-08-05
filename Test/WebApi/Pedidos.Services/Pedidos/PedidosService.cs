using Pedidos.Core;
using Pedidos.Core.Enumeraciones;
using Pedidos.Repository.Pedidos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Pedidos
{
    public class PedidosService : IPedidosService
    {
        private readonly IPedidosRepository pedidosRepository;
        public PedidosService(IPedidosRepository pedidosRepository)
        {
            this.pedidosRepository = pedidosRepository;
        }

        public bool EliminarPedido(int idPedido)
        {
            return pedidosRepository.EliminarPedido(idPedido);
        }

        public Pedido GetPedido(int idPedido, int idSucursal)
        {
            return pedidosRepository.GetPedido(idPedido, idSucursal);
        }

        public Pedido GetPedidoCompleto(int idPedido, int idSucursal)
        {
            return pedidosRepository.GetPedidoCompleto(idPedido, idSucursal);
        }

        public ICollection<Pedido> GetPedidos(DateTime fechaDesde, DateTime fechaHasta, Enumeraciones.OrderQuery order = Enumeraciones.OrderQuery.ASC, int idSucursal=0)
        {
            order = Enumeraciones.OrderQuery.DESC;
            return pedidosRepository.GetPedidos(fechaDesde, fechaHasta, order, idSucursal);
        }

        public bool InsertarPedido(Pedido pedido, List<DetallePedido> detallesPedido)
        {
            return pedidosRepository.InsertarPedido(pedido, detallesPedido);
        }

        public bool InsertarPedido(Pedido pedido)
        {
            pedido.FechaPedido = DateTime.Now;
            return pedidosRepository.InsertarPedido(pedido);
        }
    }
}
