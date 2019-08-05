using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;
using Pedidos.Repository.Sucursales;

namespace Pedidos.Services.Sucursales
{
    public class SucursalesService : ISucursalesService
    {
        private readonly ISucursalesRepository sucursalesRepository;
        public SucursalesService(ISucursalesRepository sucursalesRepository)
        {
            this.sucursalesRepository = sucursalesRepository;
        }

        public bool ActualizarSucursal(Sucursal sucursal)
        {
            return sucursalesRepository.ActualizarSucursal(sucursal);
        }
        
        public bool EliminarSucursal(int idSucursal, bool activo)
        {
            return sucursalesRepository.EliminarSucursal(idSucursal, activo);
        }

        public Sucursal GetSucursal(int id)
        {
            return sucursalesRepository.GetSucursal(idSucursal: id);
        }

        public ICollection<Sucursal> GetSucursales(bool activo)
        {
            return sucursalesRepository.GetSucursales(activo);
        }

        public ICollection<Sucursal> GetSucursales()
        {
            return sucursalesRepository.GetSucursales();
        }

        public bool InsertarSucursal(Sucursal sucursal)
        {
            return sucursalesRepository.InsertarSucursal(sucursal);
        }
    }
}
