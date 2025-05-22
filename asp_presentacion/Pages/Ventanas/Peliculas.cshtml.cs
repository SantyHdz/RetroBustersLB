using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for IHttpContextAccessor
using asp_presentacion.Pages;   // Required for SecurePageModel

namespace asp_presentacion.Pages.Ventanas
{
    public class PeliculasModel : SecurePageModel // Inherit from SecurePageModel
    {
        private readonly IPeliculasPresentacion _iPresentacion; // Made readonly

        public PeliculasModel(IPeliculasPresentacion iPresentacion,
                              IRolesPresentacion rolesPresentacion,
                              IHttpContextAccessor httpContextAccessor)
            : base(rolesPresentacion, httpContextAccessor) // Call base constructor
        {
            try
            {
                _iPresentacion = iPresentacion;
                Filtro = new Peliculas();
            }
            catch (Exception ex)
            {
                // Consider logging here or ensuring ViewData is available
                // LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Peliculas? Actual { get; set; }
        [BindProperty] public Peliculas? Filtro { get; set; }
        [BindProperty] public List<Peliculas>? Lista { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                // Session check is handled by SecurePageModel
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.Nombre = Filtro!.Nombre ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = _iPresentacion.PorNombre(Filtro!); // Use _iPresentacion
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
                Actual = new Peliculas();
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

                Task<Peliculas>? task = null;
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