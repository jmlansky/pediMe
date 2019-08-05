using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pedidos.Core;
using Pedidos.Core.Seguridad;
using Pedidos.Services.Usuarios;
using static Pedidos.Core.Enumeraciones.Enumeraciones;

namespace DeliveryOnline.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService usuariosServices;
        private readonly IConfiguration configuration;
        public UsuariosController(IUsuariosService usuariosServices, IConfiguration config)
        {
            this.usuariosServices = usuariosServices;
            configuration = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario body)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(body.Nombre) || string.IsNullOrEmpty(body.Contraseña)
                    || string.IsNullOrEmpty(body.NombreUsuario))
                    return BadRequest();

                if (usuariosServices.InsertarUsuario(body))
                    return Ok();
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] Usuario body)
        {
            if (usuariosServices.ActualizarUsuario(body))
                return Ok();
            return NotFound();
        }

        [HttpPost]
        [ActionName("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] object body)
        {
            try
            {
                var usuario = JsonConvert.DeserializeObject<Sesion>(body.ToString());
                if (usuario == null || string.IsNullOrWhiteSpace(usuario.Password) || string.IsNullOrWhiteSpace(usuario.Usuario))
                    return BadRequest("usuario en blanco o contraseña en blanco o error al crear la sesión");

                var usuarioGuardado = usuariosServices.GetUsuario(usuario.Usuario);
                if (usuarioGuardado != null && usuarioGuardado.Contraseña != null && usuarioGuardado.Contraseña.ToLower().Equals(usuario.Password.ToLower()))
                {
                    var account = GenerateAccountResponse(usuarioGuardado);
                    return Ok(JsonConvert.SerializeObject(account));
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ActionName("Logout")]
        public IActionResult Logout([FromBody] object body)
        {
            try
            {
                var account = JsonConvert.DeserializeObject<Account>(body.ToString());
                //TODO: eliminar todos los tokens del usuario account.NombreUsuario

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ActionName("GetUsuario")]
        public IActionResult GetUsuario([FromBody] object nombreUsuario)
        {
            var usuario = usuariosServices.GetUsuario(nombreUsuario.ToString());
            if (usuario != null)
                return Ok(JsonConvert.SerializeObject(usuario));
            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Get()
        {
            var result = usuariosServices.GetUsuarios();
            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpGet]
        public IActionResult GetUsuariosDeUnaEmpresa(int idEmpresa)
        {
            var result = usuariosServices.GetUsuariosDeUnaEmpresa(idEmpresa);
            return Ok();
        }

        private Account GenerateAccountResponse(Usuario usuario)
        {
            return new Account()
            {
                Nombre = usuario.Nombre,
                NombreUsuario = usuario.NombreUsuario,
                Token = JwtTokenBuilder(usuario),
                Rol = usuario.Rol
            };
        }

        private string JwtTokenBuilder(Usuario usuario)
        {
            //prepare key and credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration["JWT:key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:audience"], signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(5),
                claims: new Claim[] {
                    new Claim(ClaimTypes.Role, usuario.Rol )
                }
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}