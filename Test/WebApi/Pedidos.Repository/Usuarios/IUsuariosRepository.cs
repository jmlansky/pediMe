using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Usuarios
{
    public interface IUsuariosRepository
    {
        bool InsertarUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario);
        Usuario GetUsuario(string nombreUusuario);
        Usuario GetUsuario(string nombreUusuario, string contraseña);
        bool EliminarUsuario(int idUsuario, bool activo);      
        bool CambiarContraseña(string nombreUsuario, string contraseña);
        bool ExisteUsuario(string nombreUsuario);
        ICollection<Usuario> GetAllUsuarios();
        ICollection<Usuario> GetUsuariosDeUnaEmpresa(int idEmpresa);
    }
}
