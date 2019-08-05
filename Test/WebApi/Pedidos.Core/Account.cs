using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Account
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("nombreUsuario")]
        public string NombreUsuario { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("rol")]
        public string Rol { get; set; }
    }
}
