using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;

namespace Pedidos.Services.Usuarios
{
    public interface IUsuariosService
    {
        bool InsertarUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario);
        Usuario GetUsuario(string nombreUsuario);
        ICollection<Usuario> GetUsuarios();
        ICollection<Usuario> GetUsuariosDeUnaEmpresa(int idEmpresa);
    }
}
