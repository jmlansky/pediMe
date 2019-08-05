using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using Pedidos.Repository.DetallesCombo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Pedidos.Repository.Combos
{
    public class CombosRepository : BaseRepository, ICombosRepository
    {
        private readonly IDetallesComboRepository detallesComboRepository;
        public CombosRepository(IConfiguration configuration,
            IDetallesComboRepository detallesComboRepository) : base(configuration)
        {
            this.detallesComboRepository = detallesComboRepository;
        }

        public bool ActualizarCombo(Combo combo)
        {
            using (SqlTransaction tran = OpenConnectionWithTransaction())
            {
                try
                {
                    //se "desactiva" el combo actual y se genera uno nuevo para mantener registro de los combos anteriores
                    var sql = @"update Combo set activo = 0, fechaBaja = @fechaBaja where id = @idCombo";
                    var parameters = new Dictionary<string, object>() {
                        { "@idCombo", combo.Id},
                        { "@fechaBaja", DateTime.Now}
                    };

                    ExecuteNonQuery(sql, parameters, isStoredProcedure: false, connection: tran.Connection, transaction: tran);

                    var result = InsertarNuevoCombo(combo, tran);
                    if (!result)
                        throw new Exception();

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public bool EliminarCombo(int idCombo)
        {
            var sql = "Update Combo set activo = 0, fechaBaja = @fechaBaja where id = @idCombo";
            var parameters = new Dictionary<string, object>()
            {
                {"idCombo", idCombo },
                {"fechaBaja", DateTime.Now }
            };

            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public Combo GetCombo(int idCombo)
        {
            var sql = @"select id, nombre, precioCombo, precioPedido, activo from Combo where id = @idCombo";
            var parameters = new Dictionary<string, object>() {
                {"@idCombo", idCombo }
            };
            return Get<Combo>(sql, parameters);
        }

        public ICollection<Combo> GetCombos()
        {
            var sql = @"select id, nombre, precioCombo, precioPedido, activo from Combo where activo = 1";
            return GetListOf<Combo>(sql);
        }

        public bool InsertarCombo(Combo combo)
        {
            using (SqlTransaction tran = OpenConnectionWithTransaction())
            {
                try
                {
                    InsertarNuevoCombo(combo, tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        private bool InsertarNuevoCombo(Combo combo, SqlTransaction tran)
        {
            try
            {
                var idCombo = OnInsertarCombo(combo, tran);
                OnInsertarDetallesCombo(combo.DetallesCombo.ToList(), tran, idCombo);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private long OnInsertarCombo(Combo combo, SqlTransaction tran)
        {
            var sql = @"insert into Combo 
                            (nombre, precioCombo, precioPedido, activo)
                            values (@nombre, @precioCombo, @precioPedido, 1);
                        SELECT SCOPE_IDENTITY(); ";
            var parameters = new Dictionary<string, object>()
                {
                    {"@nombre", combo.Nombre},
                    {"@precioCombo", combo.PrecioCombo},
                    {"@precioPedido", combo.PrecioPedido},
                };

            return Convert.ToInt64(ExecuteScalarQuery(sql, parameters, isStoredProcedure: false, connection: tran.Connection, transaction: tran));
        }

        private void OnInsertarDetallesCombo(List<DetalleCombo> list, SqlTransaction tran, long idCombo)
        {
            foreach (var detC in list.Where(x => x.Seleccionado == true))
            {
                detallesComboRepository.InsertarDetalleCombo(detC, idCombo, tran);
            }
        }
    }
}
