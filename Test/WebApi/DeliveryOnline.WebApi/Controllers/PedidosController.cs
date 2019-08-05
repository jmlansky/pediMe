using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Services.Combos;
using Pedidos.Services.Pedidos;
using Pedidos.Services.Productos;
using static Pedidos.Core.Enumeraciones.Enumeraciones;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosService service;
        private readonly IProductosService productosServices;
        private readonly ICombosService combosService;
        public PedidosController(IPedidosService service, IProductosService productosServices, ICombosService combosService)
        {
            this.service = service;
            this.productosServices = productosServices;
            this.combosService = combosService;
        }

        [HttpGet]
        [ActionName("GetPedidosDelDia")]
        public IActionResult GetPedidosDelDia()
        {
            var result = service.GetPedidos(DateTime.Now.AddHours(-24), DateTime.Now);
            foreach (var ped in result)
            {
                ped.FechaFormateada = ped.FechaPedido.ToString("dd/MM/yy HH:mm");
            }
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        [ActionName("GetDetalles")]
        public IActionResult GetDetalles()
        {
            var productosActivos = productosServices.GetAllProductos(activo: true);
            var combosActivos = combosService.GetCombos();
            var listaDetalles = GenerarDetallesPedido(productosActivos, combosActivos);            
            return Ok(JsonConvert.SerializeObject(listaDetalles));
        }

        [HttpPost]
        public IActionResult Post([FromBody] object body)
        {
            var pedido = JsonConvert.DeserializeObject<Pedido>(body.ToString());
            if (body == null)
                return BadRequest();

            var result = service.InsertarPedido(pedido);
            if (result)
                return Ok();

            return StatusCode(500);
        }

        private List<DetallePedido> GenerarDetallesPedido(ICollection<Producto> productosActivos, ICollection<Combo> combosActivos)
        {
            var lista = new List<DetallePedido>();
            foreach (Producto prod in productosActivos)
            {
                lista.Add(new DetallePedido()
                {
                    IdProducto = prod.IdProducto,
                    Nombre = prod.Nombre,
                    PrecioDetalleProducto = prod.Precio,
                    TipoDetallePedido = (int)TipoDetallePedido.Producto
                });
            }

            foreach (var combo in combosActivos)
            {
                lista.Add(new DetallePedido()
                {
                    IdProducto = combo.Id,
                    Nombre = combo.Nombre,
                    PrecioDetalleProducto = combo.PrecioCombo,
                    TipoDetallePedido = (int)TipoDetallePedido.Combo
                });
            }

            return lista;
        }
    }
}