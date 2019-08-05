using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Empresa
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("cuit")]
        public string Cuit { get; set; }

        [JsonProperty("activo")]
        public bool Activo { get; set; }
    }
}
