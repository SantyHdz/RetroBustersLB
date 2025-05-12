using lib_dominio.Entidades; 
using lib_dominio.Nucleo; 
using lib_presentaciones.Interfaces; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages; 
 
namespace asp_presentacion.Pages.Ventanas 
{ 
    public class AlmacenesModel : PageModel 
    { 
        private IALmacenesPresentacion? iPresentacion = null; 
 
        public AlmacenesModel(IALmacenesPresentacion iPresentacion) 
        { 
            try 
            { 
                this.iPresentacion = iPresentacion; 
                Filtro = new Almacenes(); 
            } 
            catch (Exception ex) 
            { 
                LogConversor.Log(ex, ViewData!); 
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
                var variable_session = HttpContext.Session.GetString("Usuario"); 
                if (String.IsNullOrEmpty(variable_session)) 
                { 
                    HttpContext.Response.Redirect("/"); 
                    return; 
                } 
 
                Filtro!.Ubicacion = Filtro!.Ubicacion ?? "";
                Filtro!.Capacidad = Filtro.Capacidad != 0 ? Filtro.Capacidad : 0;

 
                Accion = Enumerables.Ventanas.Listas; 
                var task = this.iPresentacion!.PorUbicacion(Filtro!); 
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
            try 
            { 
                Accion = Enumerables.Ventanas.Editar; 
 
                Task<Almacenes>? task = null; 
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