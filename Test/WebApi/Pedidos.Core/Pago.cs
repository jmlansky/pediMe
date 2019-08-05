using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Pago
    {
        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("demora")]
        public int Demora { get; set; }

        [JsonProperty("vuelto")]
        public double Vuelto { get; set; }

        [JsonProperty("notas")]
        public string Notas { get; set; }

        [JsonProperty("promocion")]
        public Promocion Promocion { get; set; } = new Promocion();

        [JsonProperty("pago")]
        public double Abono { get; set; }
    }
}
