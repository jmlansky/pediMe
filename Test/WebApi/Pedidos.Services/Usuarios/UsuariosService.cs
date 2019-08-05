using Pedidos.Core;
using Pedidos.Repository.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Usuarios
{
    public class UsuariosService: IUsuariosService
    {
        private readonly IUsuariosRepository usuariosRepository;
        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            this.usuariosRepository = usuariosRepository;
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            return usuariosRepository.ActualizarUsuario(usuario);
        }

        public Usuario GetUsuario(string nombreUsuario)
        {
            return usuariosRepository.GetUsuario(nombreUsuario);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return usuariosRepository.GetAllUsuarios();
        }

        public ICollection<Usuario> GetUsuariosDeUnaEmpresa(int idEmpresa)
        {
            return usuariosRepository.GetUsuariosDeUnaEmpresa(idEmpresa);
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            return usuariosRepository.InsertarUsuario(usuario);
        }
    }
}
