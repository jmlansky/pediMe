using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Perfiles
{
    public interface IPerfilesService
    {
        ICollection<Rol> GetPerfiles();
    }
}
