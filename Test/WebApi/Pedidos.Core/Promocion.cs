using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Promocion
    {
        public int IdPromocion { set; get; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Activo { get; set; }

        public List<DetallePromocion> ListaDetallePromo { set; get; } = new List<DetallePromocion>();
    }
}
