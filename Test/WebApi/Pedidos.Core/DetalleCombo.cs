using Newtonsoft.Json;

namespace Pedidos.Core
{
    public class DetalleCombo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("idProducto")]
        public long IdProducto { get; set; }

        [JsonProperty("idCombo")]
        public long IdCombo { get; set; }

        [JsonProperty("producto")]
        public string NombreProducto { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }

        [JsonProperty("seleccionado")]
        public bool Seleccionado { get; set; }

        [JsonProperty("cantidad")]
        public long Cantidad { get; set; }
    }
}