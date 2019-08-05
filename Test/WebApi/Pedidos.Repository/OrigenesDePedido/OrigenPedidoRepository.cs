using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Pedidos.Repository.OrigenesDePedido
{
    public class OrigenPedidoRepository : BaseRepository, IOrigenPedidoRepository
    {
        public OrigenPedidoRepository(IConfiguration configuration) : base(configuration)
        {}

        public ICollection<OrigenPedido> GetOrigenesDePedidos()
        {
            var sql = @"select idOrigen, nombre 
                        from OrigenPedido
                        where activo = 1";

            return GetListOf<OrigenPedido>(sql);
        }
    }
}
