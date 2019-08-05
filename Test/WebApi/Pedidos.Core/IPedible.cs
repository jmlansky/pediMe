using System;
using System.Collections.Generic;
using System.Text;
using static Pedidos.Core.Enumeraciones.Enumeraciones;

namespace Pedidos.Core
{
    public interface IPedible
    {
        TipoDetallePedido TipoDePedible { get; }
    }
}
