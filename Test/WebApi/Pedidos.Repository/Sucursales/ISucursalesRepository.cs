using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Sucursales
{
    public interface ISucursalesRepository
    {
        ICollection<Sucursal> GetSucursales(bool activo);
        ICollection<Sucursal> BuscarSucursales(string direccion, int activa);
        bool InsertarSucursal(Sucursal sucursal);
        bool EliminarSucursal(int idSucursal, bool activo);
        bool ActualizarSucursal(Sucursal sucursal);
        ICollection<Sucursal> GetSucursales();
        bool ActualizarSucursalAdmin(int idSucursal, bool estadoNuevo);
        Sucursal GetSucursal(int idSucursal);
    }
}
