using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Pedidos.Repository.Comun
{
    public class BaseRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = GetConnectionString();
        }

        private string GetConnectionString()
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection OpenConnection()
        {
            var sqlCon = new SqlConnection(connectionString);
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    sqlCon.Open();
                }
                catch (Exception)
                {
                    SqlConnection.ClearPool(sqlCon);
                }
            }
            if (sqlCon.State == ConnectionState.Open)
                return sqlCon;

            return null;
        }

        public SqlTransaction OpenConnectionWithTransaction()
        {
            var sqlCon = new SqlConnection(connectionString);
            sqlCon.Open();
            try
            {
                return sqlCon.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ExecuteInsertOrUpdate(string sql, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        {
            using (SqlTransaction tran = OpenConnectionWithTransaction())
            {
                try
                {
                    ExecuteNonQuery(sql, parameters, isStoredProcedure, tran.Connection, tran);
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

        //public int GetIntValue(string sql, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        //{
        //    using (SqlConnection con = OpenConnection())
        //    {
        //        try
        //        {
        //            object result = ExecuteScalarQuery(sql, parameters, isStoredProcedure, con);
        //            return Convert.ToInt32(result);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //}

        /// <summary>
        /// This is used while trying to retreive 1 value like Id or bool or whatever. When the query uses the 'ExecuteScalar' command.
        /// </summary>
        /// <typeparam name="TResult">Expected type result</typeparam>
        /// <returns>return 1 value</returns>
        public TResult GetValue<TResult>(string sql, Dictionary<string, object> parameters, bool isStoredProcedure = false)
        {
            using (SqlConnection con = OpenConnection())
            {
                try
                {
                    object result = ExecuteScalarQuery(sql, parameters, isStoredProcedure, con);
                    return (TResult)Convert.ChangeType(result, typeof(TResult));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static object ExecuteScalarQuery(string sql, Dictionary<string, object> parameters, bool isStoredProcedure, SqlConnection connection, SqlTransaction transaction = null)
        {
            return CreateSqlCommand(sql, isStoredProcedure, connection, parameters, transaction).ExecuteScalar();
        }

        public static object ExecuteNonQuery(string sql, Dictionary<string, object> parameters, bool isStoredProcedure, SqlConnection connection, SqlTransaction transaction = null)
        {
            return CreateSqlCommand(sql, isStoredProcedure, connection, parameters, transaction).ExecuteNonQuery();
        }

        public ICollection<T> GetListOf<T>(string sql, Dictionary<string, object> parameters = null, bool isStoredProcedure = false) where T : new()
        {
            var list = new List<T>();
            using (SqlConnection con = OpenConnection())
            {
                try
                {
                    SqlCommand cmd = CreateSqlCommand(sql, isStoredProcedure, con, parameters);

                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var ob = Activator.CreateInstance<T>();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (dr.GetColumnSchema().Any(x => x.ColumnName.ToLower() == property.Name.ToLower()))
                            {
                                GetProperyWithValue(dr, ob, property);
                            }
                        }

                        list.Add(ob);
                    }
                    dr.Close();
                    return list;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public T Get<T>(string sql, Dictionary<string, object> parameters, bool isStoredProcedure = false) where T : new()
        {
            using (SqlConnection con = OpenConnection())
            {
                try
                {
                    SqlCommand cmd = CreateSqlCommand(sql, isStoredProcedure, con, parameters);

                    var dr = cmd.ExecuteReader();
                    var ob = Activator.CreateInstance<T>();
                    if (dr.Read())
                    {
                        foreach (var property in typeof(T).GetProperties())
                            GetProperyWithValue(dr, ob, property);
                    }
                    dr.Close();
                    return ob;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static void GetProperyWithValue<T>(SqlDataReader dr, T ob, PropertyInfo property) where T : new()
        {
            if (dr.GetColumnSchema().Any(x => x.ColumnName.ToLower() == property.Name.ToLower()))
            {
                var columnValue = dr[property.Name];
                if (columnValue.GetType().Name != "DBNull")
                    property.SetValue(ob, columnValue);
            }
        }

        private static SqlCommand CreateSqlCommand(string sql, bool isStoredProcedure, SqlConnection connection, Dictionary<string, object> parameters = null, SqlTransaction transaction = null)
        {
            var sqlCommand = new SqlCommand()
            {
                Connection = connection,
                CommandText = sql,
                CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                Transaction = transaction
            };

            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            return sqlCommand;
        }
    }
}
