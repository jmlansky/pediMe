using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.Services.Empresas;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresasService empresasService;
        public EmpresasController(IEmpresasService empresasService)
        {
            this.empresasService = empresasService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = empresasService.GetEmpresas();
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return NotFound();

            var result = empresasService.GetEmpresa(idEmpresa: id);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            if (empresasService.Delete(idEmpresa: id))
                return Ok();
            else
                return StatusCode(500, "Error en la eliminación de la empresa.");
        }
    }
}