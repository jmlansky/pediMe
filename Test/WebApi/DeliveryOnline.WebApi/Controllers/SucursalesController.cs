using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Services.Sucursales;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalesService service;
        public SucursalesController(ISucursalesService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Obtener todas las sucursales, sin importar si estan activas o no
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = service.GetSucursales();
            return Ok(JsonConvert.SerializeObject(result));
        }

        /// <summary>
        /// Obtener todas las sucursales segun el estado activo o inactivo
        /// </summary>        
        [HttpGet]
        [ActionName("GetActivos")]
        public IActionResult Get(bool activo)
        {
            var result = service.GetSucursales(activo);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSucursal(int id)
        {
            var result = service.GetSucursal(id);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public IActionResult Post([FromBody] object body)
        {
            var sucursal = JsonConvert.DeserializeObject<Sucursal>(body.ToString());
            var resultado = false;
            if (sucursal.Id == 0)
                resultado = service.InsertarSucursal(sucursal);
            else
                resultado = service.ActualizarSucursal(sucursal);


            return Ok(resultado);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            if (service.EliminarSucursal(id, activo: false ))
                return Ok();
            return NotFound();
        }
    }
}