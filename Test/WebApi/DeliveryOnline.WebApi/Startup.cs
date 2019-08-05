using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pedidos.Repository.Clientes;
using Pedidos.Repository.Combos;
using Pedidos.Repository.DetallesCombo;
using Pedidos.Repository.DetallesPedido;
using Pedidos.Repository.OrigenesDePedido;
using Pedidos.Repository.Pedidos;
using Pedidos.Repository.Perfiles;
using Pedidos.Repository.Productos;
using Pedidos.Repository.Promociones;
using Pedidos.Repository.Sucursales;
using Pedidos.Repository.Usuarios;
using Pedidos.Services.Clientes;
using Pedidos.Services.Combos;
using Pedidos.Services.DetallesCombo;
using Pedidos.Services.DetallesPedido;
using Pedidos.Services.OrigenesDePedido;
using Pedidos.Services.Pedidos;
using Pedidos.Services.Perfiles;
using Pedidos.Services.Productos;
using Pedidos.Services.Promociones;
using Pedidos.Services.Sucursales;
using Pedidos.Services.Usuarios;

namespace DeliveryOnline.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });

            //Add Token validation Parameters
            var tokenParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = Configuration["JWT:issuer"],
                ValidAudience = Configuration["JWT:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(Configuration["JWT:key"]))
            };
            //Add JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtconfig =>
                {
                    jwtconfig.TokenValidationParameters = tokenParams;
                });

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);


            //configure DataBase
            services.AddDbContext<DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add Services injection dependency
            AddServices(services);

            //Add Repositories injection dependency
            AddRepositories(services);

        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddSingleton<IClientesRepository, ClientesRepository>();
            services.AddSingleton<IProductosRepository, ProductosRepository>();
            services.AddSingleton<IDetallesPedidoRepository, DetallesPedidoRepository>();
            services.AddSingleton<IPedidosRepository, PedidosRepository>();
            services.AddSingleton<IRolesRepository, RolesRepository>();
            services.AddSingleton<IPromocionesRepository, PromocionesRepository>();
            services.AddSingleton<ISucursalesRepository, SucursalesRepository>();
            services.AddSingleton<IUsuariosRepository, UsuariosRepository>();
            services.AddSingleton<IOrigenPedidoRepository, OrigenPedidoRepository>();
            services.AddSingleton<ICombosRepository, CombosRepository>();
            services.AddSingleton<IDetallesComboRepository, DetallesComboRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IClientesService, ClientesService>();
            services.AddSingleton<IProductosService, ProductosService>();
            services.AddSingleton<ISucursalesService, SucursalesService>();
            services.AddSingleton<IUsuariosService, UsuariosService>();
            services.AddSingleton<IPerfilesService, PerfilesService>();
            services.AddSingleton<IPromocionesService, PromocionesService>();
            services.AddSingleton<IDetallesPedidoService, DetallesPedidoService>();
            services.AddSingleton<IPedidosService, PedidosService>();
            services.AddSingleton<IOrigenPedidoService, OrigenPedidoService>();
            services.AddSingleton<ICombosService, CombosService>();
            services.AddSingleton<IDetallesComboService, DetallesComboService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
