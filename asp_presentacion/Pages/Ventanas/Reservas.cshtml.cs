using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_presentacion.Pages.Ventanas
{
    public class ReservasModel : SecurePageModel // Inherit from SecurePageModel
    {
        private readonly IReservasPresentacion _iPresentacion;
        private readonly IMiembrosPresentacion _iMiembrosPresentacion;
        private readonly IPeliculasPresentacion _iPeliculasPresentacion;
        private readonly IConsolasPresentacion _iConsolasPresentacion;
        private readonly IEmpleadosPresentacion _iEmpleadosPresentacion;

        public ReservasModel(IReservasPresentacion iPresentacion,
                             IMiembrosPresentacion iMiembrosPresentacion,
                             IPeliculasPresentacion iPeliculasPresentacion,
                             IConsolasPresentacion iConsolasPresentacion,
                             IEmpleadosPresentacion iEmpleadosPresentacion,
                             IRolesPresentacion rolesPresentacion,
                             IHttpContextAccessor httpContextAccessor)
            : base(rolesPresentacion, httpContextAccessor) // Call base constructor
        {
            try
            {
                _iPresentacion = iPresentacion;
                _iMiembrosPresentacion = iMiembrosPresentacion;
                _iPeliculasPresentacion = iPeliculasPresentacion;
                _iConsolasPresentacion = iConsolasPresentacion;
                _iEmpleadosPresentacion = iEmpleadosPresentacion;
                Filtro = new Reservas();
            }
            catch (Exception ex)
            {
                // Consider logging here or ensuring ViewData is available
                // LogConversor.Log(ex, ViewData!);
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
        public List<Reservas>? UserReservations { get; set; }

        public virtual void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                CargarCombox(); // Moved to the beginning

                // Session check is handled by SecurePageModel
                var variable_session = HttpContext.Session.GetString("Usuario");
                if (String.IsNullOrEmpty(variable_session))
                {
                    HttpContext.Response.Redirect("/");
                    return;
                }

                Filtro!.Estado = Filtro!.Estado ?? "";

                Accion = Enumerables.Ventanas.Listas;
                var task = _iPresentacion.PorEstado(Filtro!); // Use _iPresentacion
                task.Wait();
                Lista = task.Result;
                Actual = null;

                if (CurrentUserRoleName == "Usuario")
                {
                    UserReservations = new List<Reservas>();
                }
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
                var taskmiembros = _iMiembrosPresentacion.Listar(); // Use _iMiembrosPresentacion
                taskmiembros.Wait();
                Miembros = taskmiembros.Result;
                
                var taskpeliculas = _iPeliculasPresentacion.Listar(); // Use _iPeliculasPresentacion
                taskpeliculas.Wait();
                Peliculas = taskpeliculas.Result;
                
                var taskconsolas = _iConsolasPresentacion.Listar(); // Use _iConsolasPresentacion
                taskconsolas.Wait();
                Consolas = taskconsolas.Result;
                
                var taskempleados = _iEmpleadosPresentacion.Listar(); // Use _iEmpleadosPresentacion
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
            if (!IsAdmin)
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Reservas();
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
            if (!IsAdmin && CurrentUserRoleName != "Usuario")
            {
                OnPostBtRefrescar(); 
                return;
            }
            try
            {
                Accion = Enumerables.Ventanas.Editar;

                Task<Reservas>? task = null;
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
        
        /*public decimal CalcularTotalReserva(Reservas reserva)
        {
            var pelicula = reserva._Pelicula?.Total ?? 0;
            var consola = reserva._Consola?.Total ?? 0;
            var snacks = reserva._Reservas_Snacks?.Sum(rs => rs._Snack?.Precio * rs.Cantidad) ?? 0;

            return (pelicula + consola + snacks) * reserva.Duracion;
        }*/

        
    }
}