using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class DetallePedido
    {
        public DetallePedido()
        {
            IdDetallePedido = 0;
            IdPedido = 0;
            IdProducto = 0;
            CantidadProductoDetallePedido = 0;
            ObservacionDetallePedido = string.Empty;
        }
        public DetallePedido(DetallePedido dp)
        {
            IdDetallePedido = dp.IdDetallePedido;
            IdPedido = dp.IdPedido;
            IdProducto = dp.IdProducto;
            CantidadProductoDetallePedido = dp.CantidadProductoDetallePedido;
            ObservacionDetallePedido = dp.ObservacionDetallePedido;
        }

        [JsonProperty("idDetalle")]
        public long IdDetallePedido { set; get; }

        [JsonProperty("tipoDetallePedido")]
        public int TipoDetallePedido { get; set; }

        [JsonProperty("idPedido")]
        public long IdPedido { set; get; }

        [JsonProperty("idProducto")]
        public long IdProducto { set; get; }

        [JsonProperty("cantidad")]
        public int CantidadProductoDetallePedido { set; get; }

        [JsonProperty("observaciones")]
        public string ObservacionDetallePedido { set; get; }       
        public ICollection<Precio> ListaPrecios { get; set; } = new List<Precio>();

        [JsonProperty("nombre")]
        public string Nombre { set; get; }

        [JsonProperty("precio")]
        public decimal PrecioDetalleProducto { set; get; }

        [JsonProperty("seleccionado")]
        public bool Seleccionado { get; set; } = false;
    }
}
