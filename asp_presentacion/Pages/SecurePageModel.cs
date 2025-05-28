using Microsoft.AspNetCore.Mvc.RazorPages;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asp_presentacion.Pages
{
    public class SecurePageModel : PageModel
    {
        private readonly IRolesPresentacion _rolesPresentacion;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string CurrentUserRoleName { get; private set; } = "Desconocido";
        public bool IsAdmin { get; private set; } = false;
        public bool IsUsuario => CurrentUserRoleName.Equals("Usuario", StringComparison.OrdinalIgnoreCase);


        public SecurePageModel(IRolesPresentacion rolesPresentacion, IHttpContextAccessor httpContextAccessor)
        {
            _rolesPresentacion = rolesPresentacion;
            _httpContextAccessor = httpContextAccessor;
        }
    
        public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
{
    CurrentUserRoleName = "Desconocido";
    IsAdmin = false;

    var session = _httpContextAccessor.HttpContext?.Session;

    if (session != null)
    {
        var sessionString = session.GetString("Usuario");

        if (!string.IsNullOrEmpty(sessionString))
        {
            try
            {
                var usuario = JsonConversor.ConvertirAObjeto<Usuarios>(sessionString);

                if (usuario != null && usuario.RolId > 0)
                {
                    switch (usuario.RolId)
                    {
                        case 1:
                            CurrentUserRoleName = "Admin";
                            IsAdmin = true;
                            break;
                        case 2:
                            CurrentUserRoleName = "Lector";
                            break;
                        case 3:
                            CurrentUserRoleName = "Usuario";
                            break;
                        default:
                            CurrentUserRoleName = "Rol no reconocido";
                            break;
                    }
                }
            }
            catch (System.Text.Json.JsonException jsonEx)
            {
                // Log or handle error
            }
            catch (Exception ex)
            {
                // Log or handle error
            }
        }
    }

    await next();
}

    }
}
