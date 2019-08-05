using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Pedido
    {
        [JsonProperty("cliente")]
        public Cliente Cliente { get; set; }

        [JsonProperty("pago")]
        public Pago Pago { get; set; }

        [JsonProperty("detalles")]
        public ICollection<DetallePedido> ListaDetalle { set; get; } = new List<DetallePedido>();

        [JsonProperty("origenPedido")]
        public OrigenPedido Origen { get; set; }

        [JsonProperty("idCliente")]
        public long IdCliente { set; get; }

        [JsonProperty("nombreCliente")]
        public string NombreCliente { get; set; }

        [JsonProperty("idPedido")]
        public long IdPedido { set; get; }

        [JsonProperty("total")]
        public decimal MontoTotalPedido { set; get; }
                
        public DateTime FechaPedido { set; get; }

        [JsonProperty("fecha")]
        public string FechaFormateada { get; set; }        

        [JsonProperty("observaciones")]
        public string ObservacionPedido { set; get; }

        [JsonProperty("domicilio")]
        public string DomicilioEntregaPedido { set; get; }

        //public ICollection<Promocion> ListaPromociones { set; get; } = new List<Promocion>();

        //public decimal Vuelto { set; get; }
        //public decimal CostoEnvio { set; get; }

        [JsonProperty("abono")]
        public decimal MontoAbono { set; get; }

        [JsonProperty("demora")]
        public int TiempoDemora { set; get; }
        public int Descuento { set; get; }

        //[JsonProperty("telefono")]
        //public string TelefonoCliente { set; get; }

        //[JsonProperty("idEmpresa")]
        //public int IdEmpresa { get; set; }

        [JsonProperty("idSucursal")]
        public int IdSucursal { set; get; }

        //[JsonProperty("activo")]
        //public bool Activo { get; set; }


        //public void AgregarPromocion(Promocion promo)
        //{
//ListaPromociones.Add(promo);
        //}
    }
}
