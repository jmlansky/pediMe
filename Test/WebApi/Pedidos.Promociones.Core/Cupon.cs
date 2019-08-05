using System;

namespace Pedidos.Promociones.Core
{
    public class Cupon
    {
        public Cupon()
        {
        }

        public Cupon(Promocion promo)
        {
            Promo = promo;
        }

        public DateTime ValidoDesde { get; set; }
        public DateTime ValidoHasta { get; set; }
        public eTipoDePromocion TipoDePromocion { get; set; }
        public decimal Descuento { get; set; }
        public Promocion Promo { get; set; }
    }
}