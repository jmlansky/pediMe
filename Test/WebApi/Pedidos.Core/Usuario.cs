using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Usuario
    {
        public long Id { set; get; }

        [JsonProperty("nombre"), JsonRequired]
        public string Nombre { set; get; } = string.Empty;

        [JsonProperty("nombreUsuario"), JsonRequired]
        public string NombreUsuario { set; get; } = string.Empty;

        public string Perfil { set; get; } = string.Empty;

        [JsonProperty("empresa"), JsonRequired]
        public string Empresa { get; set; } = string.Empty;

        [JsonProperty("idEmpresa")]
        public long IdEmpresa { get; set; } 

        public long IdSucursal { set; get; }
        public string Sucursal { set; get; } = string.Empty;

        [JsonProperty("password"), JsonRequired]
        public string Contraseña { set; get; } = string.Empty;

        public long IdRol { set; get; }
        [JsonProperty("rol")]
        public string Rol { get; set; } = string.Empty;

        public bool Activo { set; get; }

        [JsonProperty("telefono"), JsonRequired]
        public string Telefono { set; get; } = string.Empty;

        [JsonProperty("domicilio"), JsonRequired]
        public string Domicilio { set; get; } = string.Empty;
    }
}
