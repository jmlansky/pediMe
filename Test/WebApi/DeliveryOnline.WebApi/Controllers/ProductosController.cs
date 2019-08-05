using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Services.Productos;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService productosServices;
        public ProductosController(IProductosService productosServices)
        {
            this.productosServices = productosServices;
        }

        [HttpPost]
        public IActionResult Post([FromBody] object body)
        {
            try
            {
                var prod = JsonConvert.DeserializeObject<Producto>(body.ToString());

                if (string.IsNullOrWhiteSpace(prod.Nombre) || prod.Precio <= 0)
                    return BadRequest();

                if (prod.IdProducto > 0)
                    if (productosServices.ActualizarProducto(prod))
                        return Ok();

                if (productosServices.InsertarProducto(prod))
                    return Ok();

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //[HttpPut]
        //public IActionResult Put([FromBody] Producto body)
        //{
        //    if (productosServices.ActualizarProducto(body))
        //        return Ok();
        //    return NotFound();
        //}

        //[HttpGet]
        //public ActionResult<string> GetNombresProductos()
        //{
        //    var result = productosServices.GetAllNombresProductos();
        //    if (result.Any())
        //        return JsonConvert.SerializeObject(result);
        //    return NotFound();
        //}

        [HttpGet]
        public IActionResult Get()
        {
            var result = productosServices.GetAllProductos();
            if (result.Any())
                return Ok(JsonConvert.SerializeObject(result));
            return NotFound();
        }

        [HttpGet]
        [ActionName("GetActivos")]
        public IActionResult GetActivos()
        {
            var result = productosServices.GetAllProductos(activo: true);
            return Ok(JsonConvert.SerializeObject(result));
        }

        //[HttpGet]
        //public ActionResult<string> GetProducto([FromBody] int idProducto)
        //{
        //    var result = productosServices.GetProducto(idProducto);
        //    if (result != null)
        //        return JsonConvert.SerializeObject(result);
        //    return NotFound();
        //}

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            if (productosServices.BorrarProducto(id))
                return Ok();
            return NotFound();
        }

        [HttpPut]
        [ActionName("Activar")]
        [Route("{id}")]
        public IActionResult Activar(int id)
        {
            if (id <= 0)
                return NotFound();

            if (productosServices.ActivarProducto(id))
                return Ok();
            return NotFound();
        }
    }
}