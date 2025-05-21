using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class Reservas_SnacksModel : PageModel
    {
        private IReservas_SnacksPresentacion? iPresentacion = null;
        private ISnacksPresentacion? iSnacksPresentacion = null;
        private IReservasPresentacion? iReservasPresentacion = null;

        public Reservas_SnacksModel(IReservas_SnacksPresentacion iPresentacion,
            ISnacksPresentacion iSnacksPresentacion,
            IReservasPresentacion iReservasPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iSnacksPresentacion = iSnacksPresentacion;
                this.iReservasPresentacion = iReservasPresentacion;
                Filtro = new Reservas_Snacks();
            } 
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
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
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.Cantidad = Filtro!.Cantidad ?? -1;
                Filtro._Snack = Filtro._Snack ?? new Snacks();


                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.PorCantidad(Filtro!);
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
                var tasksnacks = this.iSnacksPresentacion!.Listar();
                tasksnacks.Wait();
                Snacks = tasksnacks.Result;
                
                var taskreservas = this.iReservasPresentacion!.Listar();
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
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Task<Reservas_Snacks>? task = null;
                if (Actual!.Id == 0)
                    task = this.iPresentacion!.Guardar(Actual!)!;
                else
                    task = this.iPresentacion!.Modificar(Actual!)!;
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
            try
            {
                var task = this.iPresentacion!.Borrar(Actual!);
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