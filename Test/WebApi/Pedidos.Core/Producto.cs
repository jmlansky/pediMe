using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Pedidos.Core.Enumeraciones;
using static Pedidos.Core.Enumeraciones.Enumeraciones;

namespace Pedidos.Core
{
    public class Producto: IPedible
    {
        public Producto()
        {
            IdProducto = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }

        public Producto(int idProducto, string nombreProducto, string observacionProducto, decimal precioX1, decimal precioX2, decimal precioX3)
        {
            IdProducto = idProducto;
            Nombre = nombreProducto;
            Descripcion = observacionProducto;
        }

        public long IdProducto { set; get; }
        public string Nombre { set; get; }
        public decimal Precio { set; get; }        
        public ICollection<Precio> ListaPrecios { get; set; } = new List<Precio>();
        public string Descripcion { set; get; }
        public bool Activo { get; set; }

        [JsonProperty("tipoDePedible")]
        public TipoDetallePedido TipoDePedible => TipoDetallePedido.Combo;
    }
}
