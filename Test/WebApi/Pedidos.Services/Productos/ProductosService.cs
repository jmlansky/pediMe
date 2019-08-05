using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pedidos.Core;
using Pedidos.Repository.Productos;

namespace Pedidos.Services.Productos
{
    public class ProductosService : IProductosService
    {
        private readonly IProductosRepository productosRepository;
        public ProductosService(IProductosRepository productosRepository)
        {
            this.productosRepository = productosRepository;
        }

        public bool ActivarProducto(int idProducto)
        {
            return productosRepository.ActivarProducto(idProducto);
        }

        public bool ActualizarProducto(Producto prod)
        {
            return productosRepository.ActualizarProducto(prod);
        }

        public bool BorrarProducto(int idProducto)
        {
            return productosRepository.BorrarProducto(idProducto);
        }

        public Dictionary<long, string>GetAllNombresProductos()
        {
            var diccionario = new Dictionary<long, string>();
            productosRepository.GetAllNombresProductos().ToList().ForEach(prod => diccionario.Add(prod.IdProducto, prod.Nombre));
            return diccionario;
        }

        public ICollection<Producto> GetAllProductos()
        {
            return productosRepository.GetAllProductos();
        }

        public ICollection<Producto> GetAllProductos(bool activo)
        {
            return productosRepository.GetAllProductos(activo);
        }

        public Producto GetProducto(int idProducto)
        {
            return productosRepository.GetProducto(idProducto);
        }

        public bool InsertarProducto(Producto prod)
        {
            return productosRepository.InsertarProducto(prod);
        }
    }
}
