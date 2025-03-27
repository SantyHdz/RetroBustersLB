using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_repositorios.Implementaciones;

public class ConexionEF3
    {
        private string string_conexion = "server=SANTY\\SQLEXPRESS;database=RetroBusters;Integrated Security=True;TrustServerCertificate=true;";
        // server=localhost;database=db_instrumentos;uid=sa;pwd=Clas3sPrO2024_!;TrustServerCertificate=true;
        // server=localhost;database=db_instrumentos;Integrated Security=True;TrustServerCertificate=true;

        public void ConexionBasica()
        {
            var conexion = new Conexion3();
            conexion.StringConexion = this.string_conexion;
            
// Obtener y mostrar Almacenes
var listaAlmacenes = conexion.Almacenes!.ToList();
foreach (var elementoAlmacen in listaAlmacenes)
{
    Console.WriteLine($"Id: {elementoAlmacen.Id_bodega}, " +
                      $"Ubicación: {elementoAlmacen.Ubicacion_bodega}, " +
                      $"Capacidad: {elementoAlmacen.Capacidad_bodega}");
}

// Obtener y mostrar Miembros
var listaMiembros = conexion.Miembros!.ToList();
foreach (var elementoMiembro in listaMiembros)
{
    Console.WriteLine($"Id: {elementoMiembro.Id_miembros}, " +
                      $"Nombre: {elementoMiembro.Nombre}, " +
                      $"Fecha de Registro: {elementoMiembro.Fecha_registro}, " +
                      $"Nivel: {elementoMiembro.Nivel}, " +
                      $"Puntos: {elementoMiembro.Puntos}");
}

// Obtener y mostrar Empleados
var listaEmpleados = conexion.Empleados!.ToList();
foreach (var elementoEmpleado in listaEmpleados)
{
    Console.WriteLine($"Id: {elementoEmpleado.Id_empleados}, " +
                      $"Nombre: {elementoEmpleado.Nombre_empleado}, " +
                      $"Cargo: {elementoEmpleado.Cargo_empleado}, " +
                      $"Sueldo: {elementoEmpleado.Sueldo}, " +
                      $"Fecha de Contratación: {elementoEmpleado.Fecha_contratacion}");
}

// Obtener y mostrar Envios
var listaEnvios = conexion.Envios!.ToList();
foreach (var elementoEnvio in listaEnvios)
{
    Console.WriteLine($"Id: {elementoEnvio.Id_envios}, " +
                      $"Estado: {elementoEnvio.Estado}, " +
                      $"Dirección: {elementoEnvio.Direccion}, " +
                      $"Transportadora: {elementoEnvio.Transportadora}, " +
                      $"EmpleadoId: {elementoEnvio.empleados}");
}

// Obtener y mostrar Reservas
var listaReservas = conexion.Reservas!.ToList();
foreach (var elementoReserva in listaReservas)
{
    Console.WriteLine($"Id: {elementoReserva.Id_Reserva}, " +
                      $"Fecha de Reserva: {elementoReserva.Fecha_Reserva}, " +
                      $"Estado: {elementoReserva.Estado}, " +
                      $"MiembroId: {elementoReserva.MiembroId}, " +
                      $"PeliculaId: {elementoReserva.PeliculaId}, " +
                      $"ConsolaId: {elementoReserva.ConsolaId}, " +
                      $"EmpleadoId: {elementoReserva.EmpleadoId}");
}

// Obtener y mostrar Peliculas
var listaPeliculas = conexion.Peliculas!.ToList();
foreach (var elementoPelicula in listaPeliculas)
{
    Console.WriteLine($"Id: {elementoPelicula.Id_pelicula}, " +
                      $"Nombre: {elementoPelicula.Nombre_pelicula}, " +
                      $"Género: {elementoPelicula.Genero_Pelicula}, " +
                      $"Fecha de Estreno: {elementoPelicula.Fecha_Estreno}, " +
                      $"Estado: {elementoPelicula.Estado_pelicula}");
}

// Obtener y mostrar Consolas
var ListaConsolas = conexion.Consolas!.ToList();
foreach (var elementoC in ListaConsolas)
{
    Console.WriteLine($"Id: {elementoC.Id_consola}, " +
                      $"Nombre: {elementoC.Tipo_consola}, " +
                      $"Género: {elementoC.Marca_consola}, " +
                      $"Fecha de Estreno: {elementoC.Estado_consola}, " +
                      $"Estado: {elementoC.almacen}");
}

// Obtener y mostrar Snacks
var listaSnacks = conexion.Snacks!.ToList();
foreach (var elementoSnack in listaSnacks)
{
    Console.WriteLine($"Id: {elementoSnack.Id_Snack}, " +
                      $"Nombre: {elementoSnack.Nombre}, " +
                      $"Precio: {elementoSnack.Precio}, " +
                      $"Stock: {elementoSnack.Stock}");
}

// Obtener y mostrar Reservas_Snacks
var listaReservasSnacks = conexion.Reservas_Snacks!.ToList();
foreach (var elementoReservaSnack in listaReservasSnacks)
{
    Console.WriteLine($"Id: {elementoReservaSnack.Id_Reservas_Snacks}, " +
                      $"SnackId: {elementoReservaSnack.SnackId}, " +
                      $"Reserva: {elementoReservaSnack.Reserva}");
}
        }

        /*public void ConexionInsert()
        {
            var conexion = new Conexion3();
            conexion.StringConnection = this.string_conexion;

            var tipo = conexion.Tipos!.FirstOrDefault(x => x.Nombre == "Viento");

            var instrumento = new Instrumentos();
            instrumento.Codigo = "TS-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            instrumento.Nombre = "Prueba";
            instrumento.Cantidad = 2;
            instrumento.Fecha = DateTime.Now;
            instrumento.Tipo = tipo!.Id;
            instrumento.Activo = false;

            conexion.Instrumentos!.Add(instrumento);
            conexion.SaveChanges();
        }
    }*/

    public partial class Conexion3 : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Almacenes>? Almacenes { get; set; }
        public DbSet<Miembros>? Miembros { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Envios>? Envios { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Peliculas>? Peliculas { get; set; }
        public DbSet<Consolas>? Consolas { get; set; }
        public DbSet<Snacks>? Snacks { get; set; }
        public DbSet<Reservas_Snacks>? Reservas_Snacks { get; set; }
    }
}