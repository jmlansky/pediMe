using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Promociones.Core.CalculoDePromociones
{
    public interface ICalculoDePromocion
    {
        decimal Calcular(Promocion promo, Cupon cupon);
    }
}
