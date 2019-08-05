using Pedidos.Promociones.Core.CalculoDePromociones;
using System;
using System.Collections.Generic;

namespace Pedidos.Promociones.Core
{
    public class Promocion
    {   
        public ICalculoDePromocion CalculoDePromocion { get; set; }
        public decimal Valor { get; set; }  
       
        public decimal CalcularDescuento(Cupon cupon)
        {
            switch (cupon.TipoDePromocion)
            {
                case eTipoDePromocion.DescuentoDePorcentaje:
                    return ((CalculoDePorcentaje)CalculoDePromocion).Calcular(cupon.Promo, cupon);
                    
                case eTipoDePromocion.DescuentoDeMontoFijo:
                    return ((CalculoDeMontoFijo)CalculoDePromocion).Calcular(cupon.Promo, cupon);

                case eTipoDePromocion.DescuentoDePrecioFijo:
                    return ((CalculoDePrecioFijo)CalculoDePromocion).Calcular(cupon.Promo, cupon);

                case eTipoDePromocion.DescuentoPorCantidad:
                    return ((CalculoPorCantidad)CalculoDePromocion).Calcular(cupon.Promo, cupon);

                default:
                    return -1;
            }
        }
    }
}
