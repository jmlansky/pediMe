using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Perfiles
{
    public class RolesRepository : BaseRepository, IRolesRepository
    {
        public RolesRepository(IConfiguration configuration) : base(configuration)
        { }

        public ICollection<Rol> GetPerfiles()
        {
            throw new NotImplementedException();
        }
    }
}
