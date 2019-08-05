using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Perfiles
{
    public interface IRolesRepository
    {
        ICollection<Rol> GetPerfiles();
    }
}
