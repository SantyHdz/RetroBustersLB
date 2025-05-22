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
                            var roleQueryObject = new Roles { Id = usuario.RolId };
                                List<Roles> roles = await _rolesPresentacion.PorId(roleQueryObject); 
                            
                            if (roles != null && roles.Count > 0 && !string.IsNullOrEmpty(roles[0].Nombre))
                            {
                                CurrentUserRoleName = roles[0].Nombre;
                                IsAdmin = string.Equals(CurrentUserRoleName, "Admin", StringComparison.OrdinalIgnoreCase);
                            }
                        }
                    }
                    catch (System.Text.Json.JsonException jsonEx)
                    {
                        
                    }
                    catch (Exception ex)
                    {
                        //
                    }
                }
            }
            
            await next();
        }
    }
}
