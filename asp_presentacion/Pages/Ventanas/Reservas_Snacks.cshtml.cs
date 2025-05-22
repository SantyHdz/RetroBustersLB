using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http; // Required for IHttpContextAccessor
using asp_presentacion.Pages;   // Required for SecurePageModel

namespace asp_presentacion.Pages.Ventanas
{
    public class Reservas_SnacksModel : SecurePageModel // Inherit from SecurePageModel
    {
        private readonly IReservas_SnacksPresentacion _iPresentacion;
        private readonly ISnacksPresentacion _iSnacksPresentacion;
        private readonly IReservasPresentacion _iReservasPresentacion;

        public Reservas_SnacksModel(IReservas_SnacksPresentacion iPresentacion,
                                    ISnacksPresentacion iSnacksPresentacion,
                                    IReservasPresentacion iReservasPresentacion,
                                    IRolesPresentacion rolesPresentacion,
                                    IHttpContextAccessor httpContextAccessor)
            : base(rolesPresentacion, httpContextAccessor) // Call base constructor
        {
            try
            {
                _iPresentacion = iPresentacion;
                _iSnacksPresentacion = iSnacksPresentacion;
                _iReservasPresentacion = iReservasPresentacion;
                Filtro = new Reservas_Snacks();
            }
            catch (Exception ex)
            {
                // Consider logging here or ensuring ViewData is available
                // LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Reservas_Snacks? Actual { get; set; }
        [BindProperty] public Reservas_Snacks? Filtro { get; set; }
        [BindProperty] public List<Reservas_Snacks>? Lista { get; set; }
        [BindProperty] public List<Snacks>? Snacks { get; set; }
        [BindProperty] public List<Reservas>? Reservas { get; set; }

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

                Filtro!.Cantidad = Filtro!.Cantidad ?? -1;
                Filtro._Snack = Filtro._Snack ?? new Snacks();


                Accion = Enumerables.Ventanas.Listas;
                var task = _iPresentacion.PorCantidad(Filtro!); // Use _iPresentacion
                task.Wait();
                Lista = task.Result;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
        
        private void CargarCombox()
        {
            try
            {
                var tasksnacks = _iSnacksPresentacion.Listar(); // Use _iSnacksPresentacion
                tasksnacks.Wait();
                Snacks = tasksnacks.Result;
                
                var taskreservas = _iReservasPresentacion.Listar(); // Use _iReservasPresentacion
                taskreservas.Wait();
                Reservas = taskreservas.Result;
                
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
                Actual = new Reservas_Snacks();
                CargarCombox();
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
                CargarCombox();
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

                Task<Reservas_Snacks>? task = null;
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