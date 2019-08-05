using Pedidos.Core;
using Pedidos.Repository.DetallesPedido;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.DetallesPedido
{
    public class DetallesPedidoService: IDetallesPedidoService
    {
        private readonly IDetallesPedidoRepository detallesPedidoRepository;
        public DetallesPedidoService(IDetallesPedidoRepository detallesPedidoRepository)
        {
            this.detallesPedidoRepository = detallesPedidoRepository;
        }

        public bool EliminarDetallesDePedidoPorIPedido(int idPedido)
        {
            return detallesPedidoRepository.EliminarDetallesDePedidoPorIPedido(idPedido);
        }

        public ICollection<DetallePedido> GetAllDetallesPedido(int idPedido)
        {
            return detallesPedidoRepository.GetAllDetallesPedido(idPedido);
        }

        public ICollection<DetallePedido> GetAllDetallesPedidoFull(int idPedido)
        {
            return detallesPedidoRepository.GetAllDetallesPedidoFull(idPedido);
        }
    }
}
