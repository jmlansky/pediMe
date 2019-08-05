using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Productos
{
    public interface IProductosService
    {
        ICollection<Producto> GetAllProductos();
        ICollection<Producto> GetAllProductos(bool activo);
        Dictionary<long, string> GetAllNombresProductos();
        Producto GetProducto(int idProducto);
        bool InsertarProducto(Producto prod);
        bool BorrarProducto(int idProducto);
        bool ActivarProducto(int idProducto);
        bool ActualizarProducto(Producto prod);
    }
}
