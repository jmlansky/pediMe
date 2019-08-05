using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;
using Pedidos.Repository.Empresas;

namespace Pedidos.Services.Empresas
{
    public class EmpresasService : IEmpresasService
    {
        private readonly IEmpresasRepository empresasRepository;
        public EmpresasService(IEmpresasRepository empresasRepository)
        {
            this.empresasRepository = empresasRepository;
        }

        public bool Delete(int idEmpresa)
        {
            return empresasRepository.Delete(idEmpresa);
        }

        public Empresa GetEmpresa(int idEmpresa)
        {
            return empresasRepository.Get(idEmpresa);
        }

        public ICollection<Empresa> GetEmpresas()
        {
            return empresasRepository.Get();
        }
    }
}
