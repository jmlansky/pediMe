using Newtonsoft.Json;
using Pedidos.Core.Enumeraciones;
using System;
using System.Collections.Generic;
using System.Text;
using static Pedidos.Core.Enumeraciones.Enumeraciones;

namespace Pedidos.Core
{
    public class Combo: IPedible
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("valor")]
        public decimal PrecioCombo { get; set; }

        [JsonProperty("detalles")]
        public ICollection<DetalleCombo> DetallesCombo { get; set; } = new List<DetalleCombo>();

        [JsonProperty("activo")]
        public bool Activo { get; set; }

        [JsonProperty("valorTeorico")]
        public decimal PrecioPedido { get; set; }

        [JsonProperty("tipoDePedible")]
        public TipoDetallePedido TipoDePedible => TipoDetallePedido.Combo;
    }
}
