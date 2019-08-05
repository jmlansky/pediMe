using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;

namespace Pedidos.Repository.Empresas
{
    public class EmpresasRepository : BaseRepository, IEmpresasRepository
    {
        public EmpresasRepository(IConfiguration configuration): base(configuration)
        {}

        public bool Delete(int idEmpresa)
        {
            var sql = @"update empresa set activo = @activo where id = @id";
            var parameters = new Dictionary<string, object>()
            {
                { "activo", 0},
                { "id", idEmpresa}
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public Empresa Get(int idEmpresa)
        {
            var sql = @"select id, nombre, cuit, activo from empresa where id= @id";
            var parameters = new Dictionary<string, object>()
            {
                { "id", idEmpresa}
            };
            return Get<Empresa>(sql, parameters);
        }

        public ICollection<Empresa> Get()
        {
            var sql = @"select id, nombre, cuit, activo from empresa";
            return GetListOf<Empresa>(sql);
        }
    }
}
