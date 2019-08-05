using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Core.Enumeraciones
{
    public class Enumeraciones
    {
        public enum OrderQuery
        {
            ASC,
            DESC
        }

        public enum TipoDetallePedido
        {
            Producto,
            Combo
        }

        public class Roles
        {
            public const string Admin = "Administrador";
            public const string Propietario = "Propietario";
            public const string Total = "Total";
            public const string Empleado = "Empleado";
        }
    }
}
