using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Services.Combos;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]    

    public class CombosController : ControllerBase
    {
        private readonly ICombosService service;
        public CombosController(ICombosService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = service.GetCombos();
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]        
        public IActionResult GetCombo(int id)
        {
            var result = service.GetCombo(id);
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public IActionResult Post([FromBody] object body)
        {
            var combo = JsonConvert.DeserializeObject<Combo>(body.ToString());
            var resultado = false;
            if (combo.Id == 0)
                resultado = service.InsertarCombo(combo);
            else
                resultado = service.ActualizarCombo(combo);


            return Ok(resultado);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return NotFound();

            if (service.EliminarCombo(id))
                return Ok();
            return NotFound();
        }
    }
}