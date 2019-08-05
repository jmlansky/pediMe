using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Sucursales
{
    public class SucursalesRepository : BaseRepository, ISucursalesRepository
    {
        public SucursalesRepository(IConfiguration configruation) : base(configruation)
        { }

        public ICollection<Sucursal> GetSucursales(bool activo)
        {
            var sql = @"select nombre, direccion, telefono, activo, responsable, email from sucursal where activo = @activo";
            var parameters = new Dictionary<string, object>()
            {
                { "@activo", activo}
            };
            return GetListOf<Sucursal>(sql, parameters);
        }

        public ICollection<Sucursal> BuscarSucursales(string direccion, int activa)
        {
            var sql = "Sucursal_buscar";
            var parameters = new Dictionary<string, object>()
            {
                { "direccion", direccion},
                { "direccion", activa }
            };

            return GetListOf<Sucursal>(sql, parameters, true);
        }

        public bool InsertarSucursal(Sucursal sucursal)
        {
            var sql = @"insert into Sucursal (direccion, telefono, localidad, activo) 
		                values (@direccion, @telefono, @localidad, 'True')";
            var parameters = new Dictionary<string, object>()
            {
                {"@direccion", sucursal.Direccion },
                {"@telefono", sucursal.Telefono },
                {"@localidad", sucursal.Localidad},
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool EliminarSucursal(int idSucursal, bool activo)
        {
            var sql = "update sucursal set activo = @activo where id = @idSucursal";
            var parameters = new Dictionary<string, object>()
            {
                {"idSucursal", idSucursal},
                {"activo", activo }
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool ActualizarSucursal(Sucursal sucursal)
        {

            var sql = "Sucursal_delete";
            var parameters = new Dictionary<string, object>()
            {
                { "direccion", sucursal.Direccion},
                { "telefono", sucursal.Telefono },
                { "localidad", sucursal.Localidad },
                { "activa", sucursal.Activo },
                { "idSucursal", sucursal.Id },
            };
            return ExecuteInsertOrUpdate(sql, parameters, true);
        }

        public ICollection<Sucursal> GetSucursales()
        {
            var sql = @"select nombre, direccion, telefono, activo, responsable, email from sucursal order by activo desc";
            return GetListOf<Sucursal>(sql);            
        }

        public bool ActualizarSucursalAdmin(int idSucursal, bool estadoNuevo)
        {
            var sql = "Sucursal_bloquear";
            var parameters = new Dictionary<string, object>()
            {
                { "activaAdmin", estadoNuevo},
                { "idSucursal", idSucursal}
            };

            return ExecuteInsertOrUpdate(sql, parameters, true);            
        }

        public Sucursal GetSucursal(int idSucursal)
        {
            var sql = @"select nombre, direccion, telefono, activo, responsable, email from sucursal where id = @id";
            var parameters = new Dictionary<string, object>()
            {
                { "@id", idSucursal}
            };
            return Get<Sucursal>(sql, parameters);
        }
    }
}
