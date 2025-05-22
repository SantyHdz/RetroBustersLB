using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x =>
            {
                x.AllowSynchronousIO = true;
            });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });

            services.AddHttpContextAccessor();   
            services.AddDistributedMemoryCache();     
            services.AddSession(options =>       
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            // Repositorios
            services.AddScoped<IConexion, ConexionEF3.Conexion>();

            // Aplicaciones
            services.AddScoped<IAlmacenesAplicacion, AlmacenesAplicacion>();
            services.AddScoped<IConsolasAplicacion, ConsolasAplicacion>();
            services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
            services.AddScoped<IEnviosAplicacion, EnviosAplicacion>();
            services.AddScoped<IMiembrosAplicacion, MiembrosAplicacion>();
            services.AddScoped<IPeliculasAplicacion, PeliculasAplicacion>();
            services.AddScoped<IReservas_SnacksAplicacion, Reservas_SnacksAplicacion>();
            services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
            services.AddScoped<ISnacksAplicacion, SnacksAplicacion>();
            services.AddScoped<IAuditoriaAplicacion, AuditoriaAplicacion>();
            services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
            services.AddScoped<IRolesAplicacion, RolesAplicacion>();

            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseSwagger();
                // app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseSession(); 

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
