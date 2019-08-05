using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class DetallePromocion
    {
        public int IdPedido { set; get; }
        public int IdProducto { set; get; }
        public string NombreProducto { set; get; }
        public int CantidadProducto { set; get; }
        public decimal PrecioX1 { set; get; }
        public decimal PrecioX2 { set; get; }
        public decimal PrecioX3 { set; get; }
        public string ObservacionProducto { set; get; }
        public int IdDetallePromocion { set; get; }
        public decimal PrecioElegido { set; get; }
        public ICollection<Precio> ListaDePrecios { get; set; } = new List<Precio>();


        public DetallePromocion()
        {
            IdPedido = 0;
            IdProducto = 0;
            NombreProducto = string.Empty;
            CantidadProducto = 0;
            PrecioX1 = 0;
            PrecioX2 = 0;
            PrecioX3 = 0;
            ObservacionProducto = string.Empty;
            IdDetallePromocion = 0;
            PrecioElegido = 0;
        }

        public DetallePromocion(DetallePromocion dp)
        {
            IdProducto = dp.IdProducto;
            IdPedido = dp.IdPedido;
            NombreProducto = dp.NombreProducto;
            CantidadProducto = dp.CantidadProducto;
            PrecioX1 = dp.PrecioX1;
            PrecioX2 = dp.PrecioX2;
            PrecioX3 = dp.PrecioX3;
            ObservacionProducto = dp.ObservacionProducto;
            IdDetallePromocion = dp.IdDetallePromocion;
            PrecioElegido = dp.PrecioElegido;
        }

        public string getDatos()
        {
            return CantidadProducto + "- " + NombreProducto + " (" + ObservacionProducto + ").\n";
        }
    }
}
