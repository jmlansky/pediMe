using System.Collections.Generic;
using Pedidos.Core;
using Pedidos.Repository.OrigenesDePedido;

namespace Pedidos.Services.OrigenesDePedido
{
    public class OrigenPedidoService : IOrigenPedidoService
    {
        private readonly IOrigenPedidoRepository origenPedidoRepository;
        public OrigenPedidoService(IOrigenPedidoRepository origenPedidoRepository)
        {
            this.origenPedidoRepository = origenPedidoRepository;
        }

        public ICollection<OrigenPedido> GetOrigenesDePedido()
        {
            return origenPedidoRepository.GetOrigenesDePedidos();
        }
    }
}
