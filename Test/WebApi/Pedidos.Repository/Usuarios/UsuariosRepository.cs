using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Usuarios
{
    public class UsuariosRepository : BaseRepository, IUsuariosRepository
    {
        public UsuariosRepository(IConfiguration configuration) : base(configuration)
        { }

        public bool InsertarUsuario(Usuario usuario)
        {
            var sql = "Usuario_AddNew";
            var parameters = new Dictionary<string, object>()
            {
                { "nombreUsuario", usuario.NombreUsuario},
                { "contraseña", usuario.Contraseña},
                { "idRol", usuario.IdRol},
                { "idSucursal", usuario.IdSucursal},
                { "nombre", usuario.Nombre},
                { "telefono", usuario.Telefono},
                { "domicilio", usuario.Domicilio}
            };

            return ExecuteInsertOrUpdate(sql, parameters, true);
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            var sql = "Usuario_Update";
            var parameters = new Dictionary<string, object>()
            {
                {"nombreUsuario", usuario.NombreUsuario},
                {"contraseña", usuario.Contraseña},
                {"idPerfil", usuario.IdRol},
                {"idSucursal", usuario.IdSucursal},
                {"nombre", usuario.Nombre},
                {"telefono", usuario.Telefono},
                {"domicilio", usuario.Domicilio},
                {"idUsuario", usuario.Id},
                {"activo", usuario.Activo}
            };

            return ExecuteInsertOrUpdate(sql, parameters, true);
        }

        public Usuario GetUsuario(string nombreUsuario)
        {
            var sql = @"select usr.nombre, usr.nombreUsuario, usr.contraseña, rol.nombre 'Rol'
                        from usuario usr
                        INNER JOIN Rol rol
                            ON usr.idRol = rol.id
                        where nombreUsuario = @nombreUsuario";
            var parameters = new Dictionary<string, object>()
            {
                {"nombreUsuario", nombreUsuario}
            };
            return Get<Usuario>(sql, parameters);
        }

        public Usuario GetUsuario(string nombreUsuario, string contraseña)
        {
            var sql = "Usuario_getValidacion";
            var parameters = new Dictionary<string, object>()
            {
                {"nombreUsuario", nombreUsuario},
                {"contraseña", contraseña}

            };
            return Get<Usuario>(sql, parameters, true);
        }

        public bool EliminarUsuario(int idUsuario, bool activo)
        {
            var sql = "Usuario_delete";
            var parameters = new Dictionary<string, object>()
            {
                {"idUsuario", idUsuario},
                {"activo", activo}
            };

            return ExecuteInsertOrUpdate(sql, parameters, true);
        }

        public bool CambiarContraseña(string nombreUsuario, string contraseña)
        {

            var sql = "Usuario_cambiarContraseña";
            var parameters = new Dictionary<string, object>()
            {
                {"nombreUsuario", nombreUsuario},
                {"contraseña", contraseña}
            };

            return ExecuteInsertOrUpdate(sql, parameters, true);
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            var sql = "Usuario_existe";
            var parameters = new Dictionary<string, object>()
            {
                { "nombreUsuario", nombreUsuario}
            };

            return GetValue<bool>(sql, parameters, true);
        }

        public ICollection<Usuario> GetAllUsuarios()
        {
            var sql = @"select u.id, u.nombreUsuario, u.activo, u.nombre, u.telefono, u.domicilio, r.nombre 'Rol', 
                        e.nombre 'Empresa', s.nombre 'Sucursal'
                    from Usuario u
                    INNER JOIN Rol r
                        ON u.idRol = r.id
                    inner join Empresa e
                        on u.idEmpresa = e.id
                    left join Sucursal s
                        on u.idSucursal = s.id
                    where u.activo = @activo";
            var parameters = new Dictionary<string, object>()
            {
                {"activo", true},
            };
            return GetListOf<Usuario>(sql, parameters);
        }

        public ICollection<Usuario> GetUsuariosDeUnaEmpresa(int idEmpresa)
        {
            var sql = @"select u.id, u.nombreUsuario, u.activo, u.nombre, u.telefono, u.domicilio, r.nombre, 
                        e.nombre as 'Empresa', s.nombre as 'Sucursal'
                    from Usuario u
                    INNER JOIN Rol r
                        ON u.idRol = r.id
                    inner join Empresa e
                        on u.idEmpresa = r.idEmpresa
                    left join Sucursal s
                        on u.idSucursal = s.id
                    where e.id = @idEmpresa";
            var parameters = new Dictionary<string, object>()
            {
                {"idEmpresa", idEmpresa},
            };
            return GetListOf<Usuario>(sql, parameters);
        }
    }
}
