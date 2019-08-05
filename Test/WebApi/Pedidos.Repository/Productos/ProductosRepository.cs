using Microsoft.Extensions.Configuration;
using Pedidos.Core;
using Pedidos.Repository.Comun;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Productos
{
    public class ProductosRepository : BaseRepository, IProductosRepository
    {
        public ProductosRepository(IConfiguration configuration) : base(configuration)
        { }

        public ICollection<Producto> GetAllProductos()
        {
            var sql = @"select prod.idProducto, prod.nombre, prod.descripcion, prod.precio, prod.activo 
                        from Producto prod                        
                        order by activo desc, prod.nombre asc";
            return GetListOf<Producto>(sql);
        }

        public ICollection<Producto> GetAllProductos(bool activo)
        {
            var sql = @"select prod.idProducto, prod.nombre, prod.descripcion, prod.precio, prod.activo 
                        from Producto prod 
                        where activo = @activo
                        order by activo desc, prod.nombre asc";

            var parameters = new Dictionary<string, object>()
            {
                { "activo", activo}
            };

            return GetListOf<Producto>(sql, parameters);
        }

        public ICollection<Producto> GetAllNombresProductos()
        {
            var sql = "select prod.idProducto, prod.nombre from Producto prod";
            return GetListOf<Producto>(sql);
        }

        public Producto GetProducto(int idProducto)
        {
            var sql = "select prod.* from Producto prod where prod.idProducto = @idProducto";
            var parameters = new Dictionary<string, object>()
            {
                { "idProducto", idProducto}
            };

            return Get<Producto>(sql, parameters);
        }

        public bool InsertarProducto(Producto prod)
        {

            var sql = "insert into Producto (nombre, descripcion, precio, activo) values (@nombre, @descripcion, @precio, @activo)";
            var parameters = new Dictionary<string, object>()
            {
                { "nombre", prod.Nombre },
                { "descripcion", prod.Descripcion },
                { "precio", prod.Precio},
                { "activo", prod.Activo}
            };

            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool BorrarProducto(int idProducto)
        {
            var sql = "Update Producto set activo = 0 where idProducto = @idProducto";
            var parameters = new Dictionary<string, object>()
            {
                {"idProducto", idProducto }
            };

            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool ActualizarProducto(Producto prod)
        {
            var sql = @"Update Producto set nombre = @nombre, 
                                            descripcion = @descripcion, 
                                            precio = @precio, 
                                            activo = @activo 
                        where idProducto = @idProducto";

            var parameters = new Dictionary<string, object>()
            {
                { "nombre", prod.Nombre },
                { "descripcion", prod.Descripcion },
                { "precio", prod.Precio},
                { "activo", prod.Activo},
                { "idProducto", prod.IdProducto }
            };

            return ExecuteInsertOrUpdate(sql, parameters);
        }

        public bool ActivarProducto(int idProducto)
        {
            var sql = "Update Producto set activo = 1 where idProducto = @idProducto";
            var parameters = new Dictionary<string, object>()
            {
                {"idProducto", idProducto }
            };

            return ExecuteInsertOrUpdate(sql, parameters);

        }
    }
}
