using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Sucursales
{
    public interface ISucursalesService
    {
        ICollection<Sucursal> GetSucursales(bool activo);
        bool InsertarSucursal(Sucursal sucursal);
        bool EliminarSucursal(int idSucursal, bool activo);
        bool ActualizarSucursal(Sucursal sucursal);        
        ICollection<Sucursal> GetSucursales();
        Sucursal GetSucursal(int id);
    }
}
