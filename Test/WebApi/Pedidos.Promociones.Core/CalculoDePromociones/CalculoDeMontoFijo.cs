using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Promociones.Core.CalculoDePromociones
{
    public class CalculoDeMontoFijo : ICalculoDePromocion
    {
        public decimal Calcular(Promocion promo, Cupon cupon)
        {
            return promo.Valor - cupon.Descuento;
        }
    }
}
