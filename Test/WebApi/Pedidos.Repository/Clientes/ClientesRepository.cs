using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Pedidos.Repository.Comun;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Pedidos.Repository.Clientes
{
    public class ClientesRepository : BaseRepository, IClientesRepository
    {
        //constructor
        public ClientesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Cliente GetCliente(string telefono)
        {
            var sql = "select cli.idCliente, cli.nombre, cli.domicilio, cli.telefono from Cliente cli where cli.telefono = @telefono";
            var parameters = new Dictionary<string, object>()
            {
                { "telefono", telefono }
            };

            return Get<Cliente>(sql, parameters);
        }

        /// <summary>
        /// Metodo que busca un cliente en la Db a traves del ID del mismo, puede devolver null, se debe validar desde donde
        /// se lo llame
        /// </summary>
        /// <param name="pIdCliente">int Id del cliente a buscar</param>
        /// <returns>Cliente encontrado, puede devolver null</returns>
        public Cliente GetCliente(int idCliente)
        {
            var sql = "select cli.idCliente, cli.nombre, cli.domicilio, cli.telefono from Cliente cli where cli.idCliente = @idCliente";
            var parameters = new Dictionary<string, object>()
            {
                { "idCliente", idCliente }
            };

            return Get<Cliente>(sql, parameters);
        }

        public ICollection<Cliente> GetClientes()
        {
            var sql = "select id, nombre, domicilio, telefono from Cliente";
            return GetListOf<Cliente>(sql, new Dictionary<string, object>());
        }

        public ICollection<Cliente> GetClientes(string telefono)
        {
            var sql = "select idCliente, nombre, domicilio, telefono from Cliente where telefono like @telefono +'%'";
            var parameters = new Dictionary<string, object>()
            {
                { "telefono", telefono }
            };

            return GetListOf<Cliente>(sql, parameters);
        }

        /// <summary>
        /// Devuelve el id del cliente que se le envia el telefono.
        /// Según el código, esto siempre trae un valor ya que se ejecuta cuando el cliente ya existe o se lo grabo previamente antes
        /// de grabar un pedido.
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public int GetIdCliente(string telefono)
        {
            var sql = "select cli.idCliente from Cliente cli where cli.telefono = @telefono";
            var parameters = new Dictionary<string, object>()
            {
                {"telefono", telefono }
            };
            return GetValue<int>(sql, parameters);
        }       

        public bool UpdateCliente(Cliente cli)
        {
            var sql = "update Cliente set nombre = @nombre, domicilio = @domicilio, telefono = @telefono where idCliente = @idCliente";

            var parametes = new Dictionary<string, object>
            {
                { "nombre", cli.Nombre },
                { "telefono", cli.Telefono },
                { "domicilio", cli.Domicilio },
                { "idCliente", cli.Id }
            };

            return ExecuteInsertOrUpdate(sql, parametes);
        }

        public bool InsertarCliente(Cliente cli)
        {
            var sql = @"insert into Cliente(nombre, domicilio, telefono, idTipoCliente)
		                values (@nombre, @domicilio, @telefono, @idtipoCliente)";

            var parametes = new Dictionary<string, object>
            {
                { "@nombre", cli.Nombre },
                { "@domicilio", cli.Domicilio },
                { "@telefono", cli.Telefono },
                { "@idtipoCliente", cli.IdTipoCliente}
            };

            return ExecuteInsertOrUpdate(sql, parametes);
        }
    }
}