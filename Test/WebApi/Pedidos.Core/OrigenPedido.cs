using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class OrigenPedido
    {
        [JsonProperty("idOrigen")]
        public int IdOrigen { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}
