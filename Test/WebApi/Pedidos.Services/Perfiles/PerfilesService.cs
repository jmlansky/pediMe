using Pedidos.Core;
using Pedidos.Repository.Perfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Perfiles
{
    public class PerfilesService: IPerfilesService
    {
        private readonly IRolesRepository perfilesRespository;
        public PerfilesService(IRolesRepository perfilesRespository)
        {
            this.perfilesRespository = perfilesRespository;
        }

        public ICollection<Rol> GetPerfiles()
        {
            return perfilesRespository.GetPerfiles();
        }
    }
}
