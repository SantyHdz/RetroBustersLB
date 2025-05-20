using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private IReservasPresentacion? iPresentacion = null;
        private IMiembrosPresentacion? iMiembrosPresentacion = null;
        private IPeliculasPresentacion? iPeliculasPresentacion = null;
        private IConsolasPresentacion? iConsolasPresentacion = null;
        private IEmpleadosPresentacion? iEmpleadosPresentacion = null;

        public ReservasModel(IReservasPresentacion iPresentacion,
            IMiembrosPresentacion iMiembrosPresentacion, 
            IPeliculasPresentacion iPeliculasPresentacion, 
            IConsolasPresentacion iConsolasPresentacion, 
            IEmpleadosPresentacion iEmpleadosPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                this.iMiembrosPresentacion = iMiembrosPresentacion;
                this.iPeliculasPresentacion = iPeliculasPresentacion;
                this.iConsolasPresentacion = iConsolasPresentacion;
                this.iEmpleadosPresentacion = iEmpleadosPresentacion;
                Filtro = new Reservas();
            } 
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IFormFile? FormFile { get; set; }
        // public decimal TotalCalculado => CalcularTotalReserva(Actual!);
        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public List<Reservas>? Lista { get; set; }
        [BindProperty] public List<Miembros>? Miembros { get; set; }
        [BindProperty] public List<Peliculas>? Peliculas { get; set; }
        [BindProperty] public List<Consolas>? Consolas { get; set; }
        [BindProperty] public List<Empleados>? Empleados { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                //var variable_session = HttpContext.Session.GetString("Usuario");
                //if (String.IsNullOrEmpty(variable_session))
                //{
                //    HttpContext.Response.Redirect("/");
                //    return;
                //}

                Filtro!.Estado = Filtro!.Estado ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = this.iPresentacion!.PorEstado(Filtro!);
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
                var taskmiembros = this.iMiembrosPresentacion!.Listar();
                taskmiembros.Wait();
                Miembros = taskmiembros.Result;
                
                var taskpeliculas = this.iPeliculasPresentacion!.Listar();
                taskpeliculas.Wait();
                Peliculas = taskpeliculas.Result;
                
                var taskconsolas = this.iConsolasPresentacion!.Listar();
                taskconsolas.Wait();
                Consolas = taskconsolas.Result;
                
                var taskempleados = this.iEmpleadosPresentacion!.Listar();
                taskempleados.Wait();
                Empleados = taskempleados.Result;
                
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
                Actual = new Reservas();
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

                Task<Reservas>? task = null;
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
        
        /*public decimal CalcularTotalReserva(Reservas reserva)
        {
            var pelicula = reserva._Pelicula?.Total ?? 0;
            var consola = reserva._Consola?.Total ?? 0;
            var snacks = reserva._Reservas_Snacks?.Sum(rs => rs._Snack?.Precio * rs.Cantidad) ?? 0;

            return (pelicula + consola + snacks) * reserva.Duracion;
        }*/

        
    }
}