using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pedidos.Services.Clientes;
using Newtonsoft.Json;
using Pedidos.Core;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService service;
        public ClientesController(IClientesService service)
        {
            this.service = service;
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] Cliente body)
        {
            if (string.IsNullOrEmpty(body.Domicilio) || string.IsNullOrEmpty(body.Nombre) || string.IsNullOrEmpty(body.Telefono))
                return BadRequest();

            if (service.UpdateCliente(body))
                return Ok();
            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var result = service.GetClientes();           
            return JsonConvert.SerializeObject(result);            
        }
        
        [HttpGet]
        [ActionName("getCliente")]
        [HttpGet("{telefono}")]
        public ActionResult<string> GetCliente(string telefono)
        {
            if (string.IsNullOrEmpty(telefono))
                return BadRequest();

            var result = service.GetCliente(telefono);
            if (result != null && result.Id > 0)
                return JsonConvert.SerializeObject(result);
            else
            {
                result.Telefono = telefono;
                return JsonConvert.SerializeObject(result);
            }                
        }




        #region codigo a ver mas adelante

        //[HttpPost]
        //public IActionResult Post([FromBody] Cliente body)
        //{
        //    if (service.InsertarCliente(body))
        //        return Ok();
        //    else
        //        return NotFound();
        //}

        //[HttpGet]
        //public ActionResult<string> GetIdCliente([FromBody] string value)
        //{
        //    var result = service.GetIdCliente();
        //    if (result > 0)
        //        return Ok();
        //    else
        //        return NotFound();
        //}

        //[HttpGet]
        //public ActionResult<string> GetClientePorIdCliente([FromBody] int idCliente)
        //{
        //    var result = service.GetCliente(idCliente);
        //    if (result != null)
        //        return JsonConvert.SerializeObject(result);
        //    else
        //        return NotFound();
        //}

        //[HttpGet]
        //public ActionResult<string> GetClientes([FromBody] string telefono)
        //{
        //    var result = service.GetClientes(telefono);
        //    if (result != null)
        //        return JsonConvert.SerializeObject(result);
        //    else
        //        return NotFound();
        //}
        #endregion
    }
}