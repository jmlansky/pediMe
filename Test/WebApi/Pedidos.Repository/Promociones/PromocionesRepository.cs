using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Promociones
{
    public class PromocionesRepository : BaseRepository, IPromocionesRepository
    {
        public PromocionesRepository(IConfiguration configuration): base(configuration)
        {}

        public bool InsertarPromocion(Promocion promocion)
        {
            var sql = "insert into Promocion(nombre, codigo) values (@nombre, @codigo)";
            var parameters = new Dictionary<string, object>()
            {
                { "nombre", promocion.Nombre},
                { "codigo", promocion.Codigo}
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool BorrarPromocion(int idPromocion)
        {
            var sql = "update Promocion set activo = 0 where idPromocion = @idPromocion";
            var parameters = new Dictionary<string, object>()
            {                
                { "idPromocion", idPromocion}
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool ActualizarPromocion(Promocion promocion)
        {
            var sql = "update Promocion set nombre = @nombre, codigo = @codigo, activo = @activo where idPromocion = @idPromocion";
            var parameters = new Dictionary<string, object>()
            {
                { "nombre", promocion.Nombre},
                { "codigo", promocion.Codigo},
                { "activo", promocion.Activo},
                { "idPromocion", promocion.IdPromocion}
            };
            return ExecuteInsertOrUpdate(sql, parameters);
        }
    }
}
