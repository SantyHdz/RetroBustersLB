using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_presentacion.Pages.Ventanas
{
    public class UsuariosModel : SecurePageModel // Inherit from SecurePageModel
    {
        private readonly IUsuariosPresentacion _iPresentacion; // Made readonly

        // Updated constructor to include IRolesPresentacion and IHttpContextAccessor
        public UsuariosModel(IUsuariosPresentacion iPresentacion, 
                              IRolesPresentacion rolesPresentacion, 
                              IHttpContextAccessor httpContextAccessor) 
            : base(rolesPresentacion, httpContextAccessor) // Call base constructor
        {
            try
            {
                _iPresentacion = iPresentacion;
                Filtro = new Usuarios();
            } 
            catch (Exception ex)
            {
                // Consider logging here or ensuring ViewData is available if SecurePageModel doesn't handle it
                // LogConversor.Log(ex, ViewData!); // ViewData might not be fully initialized here
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Usuarios? Actual { get; set; }
        [BindProperty] public Usuarios? Filtro { get; set; }
        [BindProperty] public List<Usuarios>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.Correo = Filtro!.Correo ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = _iPresentacion.PorCorreo(Filtro!); // Use _iPresentacion
                task.Wait();
                Lista = task.Result;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            if (!IsAdmin)
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Usuarios();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            if (!IsAdmin)
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            if (!IsAdmin)
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                // Antes de editar, garantizar la vista de edición
                Accion = Enumerables.Ventanas.Editar;

                // Solo hashear si se proporcionó una contraseña (nuevo o cambiada)
                if (!string.IsNullOrWhiteSpace(Actual!.ContrasenaHash))
                {
                    Actual.ContrasenaHash = HashUtil.ComputeSha256Hash(Actual.ContrasenaHash);
                }

                Task<Usuarios>? task = null;
                if (Actual.Id == 0)
                    task = _iPresentacion.Guardar(Actual!);
                else
                    task = _iPresentacion.Modificar(Actual!);
                task.Wait();
                Actual = task.Result;
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            if (!IsAdmin)
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            if (!IsAdmin)
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                var task = _iPresentacion.Borrar(Actual!); // Use _iPresentacion
                Actual = task.Result;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}