using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Cliente
    {
        public Cliente()
        {
            Id = 0;
            Nombre = string.Empty;
            Domicilio = string.Empty;
            Telefono = string.Empty; 
        }
        public Cliente(int idCliente, string nombreCliente, string domicilioCliente, string telefonoCliente, int tipoCliente)
        {
            Id = idCliente;
            Nombre = nombreCliente;
            Domicilio = domicilioCliente;
            Telefono = telefonoCliente;
            IdTipoCliente = tipoCliente;
        }

        [JsonProperty("idCliente")]
        public long Id { set; get; }

        [JsonProperty("nombre")]
        public string Nombre { set; get; }

        [JsonProperty("domicilio")]
        public string Domicilio { set; get; }

        [JsonProperty ("telefono")]
        public string Telefono { set; get; }

        [JsonProperty]
        public int IdTipoCliente { set; get; }        
    }
}
