using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;

namespace Pedidos.Repository.Empresas
{
    public interface IEmpresasRepository
    {
        bool Delete(int idEmpresa);
        Empresa Get(int idEmpresa);
        ICollection<Empresa> Get();
    }
}
