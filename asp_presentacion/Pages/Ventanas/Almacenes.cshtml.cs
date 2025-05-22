using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_presentacion.Pages.Ventanas
{
    public class AlmacenesModel : SecurePageModel // Inherit from SecurePageModel
    {
        private readonly IAlmacenesPresentacion _iPresentacion; // Made readonly

        // Updated constructor to include IRolesPresentacion and IHttpContextAccessor
        public AlmacenesModel(IAlmacenesPresentacion iPresentacion, 
                              IRolesPresentacion rolesPresentacion, 
                              IHttpContextAccessor httpContextAccessor) 
            : base(rolesPresentacion, httpContextAccessor) // Call base constructor
        {
            try
            {
                _iPresentacion = iPresentacion;
                Filtro = new Almacenes();
            } 
            catch (Exception ex)
            {
                // Consider logging here or ensuring ViewData is available if SecurePageModel doesn't handle it
                // LogConversor.Log(ex, ViewData!); // ViewData might not be fully initialized here
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Almacenes? Actual { get; set; }
        [BindProperty] public Almacenes? Filtro { get; set; }
        [BindProperty] public List<Almacenes>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                // Session check is handled by SecurePageModel or redirects if session is not found by OnPageHandlerExecutionAsync
                // However, direct HttpContext.Session access might still be used here for other logic
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/"); // This redirect might conflict with SecurePageModel logic
                    return;
                }

                Filtro!.Ubicacion = Filtro!.Ubicacion ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = _iPresentacion.PorUbicacion(Filtro!); // Use _iPresentacion
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
                Actual = new Almacenes();
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
                Accion = Enumerables.Ventanas.Editar;

                Task<Almacenes>? task = null;
                if (Actual!.Id == 0)
                    task = _iPresentacion.Guardar(Actual!)!; // Use _iPresentacion
                else
                    task = _iPresentacion.Modificar(Actual!)!; // Use _iPresentacion
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