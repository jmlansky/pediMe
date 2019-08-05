using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Empresas
{
    public interface IEmpresasService
    {
        ICollection<Empresa> GetEmpresas();
        Empresa GetEmpresa(int idEmpresa);
        bool Delete(int idEmpresa);
    }
}
