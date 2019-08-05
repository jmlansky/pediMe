using Newtonsoft.Json;

namespace Pedidos.Core
{
    public class Sucursal
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { set; get; } = string.Empty;

        [JsonProperty("telefono")]
        public string Telefono { set; get; } = string.Empty;

        [JsonProperty("localidad")]
        public string Localidad { set; get; } = string.Empty;

        [JsonProperty("activo")]
        public bool Activo { set; get; }

        [JsonProperty("responsable")]
        public string Responsable { set; get; } = string.Empty;

        [JsonProperty("email")]
        public string Email { set; get; } = string.Empty;

    }
}
