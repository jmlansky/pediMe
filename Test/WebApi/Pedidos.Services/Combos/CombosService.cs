using Pedidos.Core;
using Pedidos.Repository.Combos;
using Pedidos.Repository.DetallesCombo;
using Pedidos.Repository.Productos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Combos
{
    public class CombosService: ICombosService
    {
        private readonly ICombosRepository combosRepository;
        private readonly IProductosRepository productosRepository;
        private readonly IDetallesComboRepository detallesComboRepository;

        public CombosService(ICombosRepository combosRepository, IProductosRepository productosRepository,
            IDetallesComboRepository detallesComboRepository)
        {
            this.combosRepository = combosRepository;
            this.detallesComboRepository = detallesComboRepository;
            this.productosRepository = productosRepository;
        }

        public bool ActualizarCombo(Combo combo)
        {
            return combosRepository.ActualizarCombo(combo);
        }

        public bool EliminarCombo(int id)
        {
            return combosRepository.EliminarCombo(id);
        }

        public Combo GetCombo(int idCombo)
        {
            var combo = combosRepository.GetCombo(idCombo);
            if (combo == null || combo.Id == 0)
                combo = new Combo();

            var listaDetallesSeleccionados = detallesComboRepository.GetDetalles(idCombo: idCombo);
            var productosActivos = productosRepository.GetAllProductos(activo: true);

            //arma la lista de detalles a devolver
            foreach (var prod in productosActivos)
            {
                combo.DetallesCombo.Add(new DetalleCombo()
                {
                    IdProducto = prod.IdProducto,
                    NombreProducto = prod.Nombre,
                    Seleccionado = false,
                    IdCombo = idCombo,
                    Valor = prod.Precio
                });
            }

            //selecciona los detalles correspondientes al monto
            foreach (var detSeleccionado in listaDetallesSeleccionados)
            {
                foreach (var detalle in combo.DetallesCombo)
                {
                    if (detalle.IdProducto == detSeleccionado.IdProducto)
                    {
                        detalle.Seleccionado = true;
                        detalle.Id = detSeleccionado.Id;
                        detalle.Cantidad = detSeleccionado.Cantidad;
                        break;
                    }
                }
            }
           
            return combo;
        }

        public ICollection<Combo> GetCombos()
        {
            return combosRepository.GetCombos();
        }

        public bool InsertarCombo(Combo combo)
        {
            return combosRepository.InsertarCombo(combo);
        }
    }
}
