using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Sesion
    {
        [JsonProperty("nombreUsuario")]
        public string Usuario{ get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
