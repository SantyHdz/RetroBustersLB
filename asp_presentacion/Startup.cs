using lib_aplicaciones.Interfaces;
using lib_dominio.Implementaciones;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;

namespace asp_presentacion
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
            // Presentaciones 
            services.AddScoped<IAlmacenesPresentacion, AlmacenesPresentacion>();
            services.AddScoped<IConsolasPresentacion, ConsolasPresentacion>();
            services.AddScoped<IMiembrosPresentacion, MiembrosPresentacion>();
            services.AddScoped<IEmpleadosPresentacion, EmpleadosPresentacion>();
            services.AddScoped<IPeliculasPresentacion, PeliculasPresentacion>();
            services.AddScoped<ISnacksPresentacion, SnacksPresentacion>();
            services.AddScoped<IEnviosPresentacion, EnviosPresentacion>();
            services.AddScoped<IReservasPresentacion, ReservasPresentacion>();
            services.AddScoped<IReservas_SnacksPresentacion, Reservas_SnacksPresentacion>();
            services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
            services.AddScoped<IRolesAplicacion, RolesImplementacion>();
            services.AddScoped<IPermisosAplicacion, PermisosImplementacion>();
            services.AddScoped<IRolPermisoAplicacion, RolPermisoAplicacion>();


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}