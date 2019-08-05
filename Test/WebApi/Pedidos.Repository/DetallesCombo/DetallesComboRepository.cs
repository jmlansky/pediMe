using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Pedidos.Repository.DetallesCombo
{
    public class DetallesComboRepository: BaseRepository, IDetallesComboRepository
    {
        public DetallesComboRepository(IConfiguration configuration) : base(configuration)
        {}

        public ICollection<DetalleCombo> GetDetalles(int idCombo)
        {
            var sql = @"select id, idCombo, idProducto, cantidad, observaciones from DetalleCombo where idCombo = @idCombo";

            var parameters = new Dictionary<string, object>()
            {
                {"@idCombo", idCombo}
            };
            return GetListOf<DetalleCombo>(sql, parameters);
        }

        public bool InsertarDetalleCombo(DetalleCombo detC, long idCombo, SqlTransaction tran)
        {

            var sql = @"insert into DetalleCombo (idCombo, idProducto, cantidad)
                        values (@idCombo, @idProducto, @cantidad)";

            var parameters = new Dictionary<string, object>()
                        {
                            {"@idCombo", idCombo},
                            {"@idProducto", detC.IdProducto},
                            {"@cantidad", detC.Cantidad}
                        };


            ExecuteNonQuery(sql, parameters, false, tran.Connection, tran);
            return true;
        }
    }
}
