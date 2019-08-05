using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Productos
{
    public interface IProductosRepository
    {
        ICollection<Producto> GetAllProductos();
        ICollection<Producto> GetAllProductos(bool activo);

        ICollection<Producto> GetAllNombresProductos();
        Producto GetProducto(int idProducto);
        bool InsertarProducto(Producto prod);
        bool BorrarProducto(int idProducto);
        bool ActualizarProducto(Producto prod);
        bool ActivarProducto(int idProducto);
    }
}
