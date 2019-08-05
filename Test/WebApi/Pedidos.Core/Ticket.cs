using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core
{
    public class Ticket
    {
        public Ticket()
        {
            Detalle = new List<string>();
            TipoEntrega = string.Empty;
            NombreCliente = string.Empty;
            Telefono = string.Empty;
            Domicilio = string.Empty;
            Obs = string.Empty;
            MontoTotal = 0;
            AbonaCon = 0;
            Vuelto = 0;
            Demora = 0;
            CostoEnvio = 0;
        }

        public ICollection<string> Detalle { set; get; } = new List<string>();
        public string TipoEntrega { set; get; }
        public string NombreCliente { set; get; }
        public string Telefono { set; get; }
        public string Domicilio { set; get; }
        public string Obs { set; get; }
        public decimal MontoTotal { set; get; }
        public decimal AbonaCon { set; get; }
        public decimal Vuelto { set; get; }
        public int Demora { set; get; }
        public decimal CostoEnvio { set; get; }
    }
}
