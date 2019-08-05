using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Services.OrigenesDePedido;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrigenPedidoController : ControllerBase
    {
        private readonly IOrigenPedidoService service;
        public OrigenPedidoController(IOrigenPedidoService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var result = service.GetOrigenesDePedido();
            return JsonConvert.SerializeObject(result);
        }
    }
}