using lib_dominio.Entidades;
namespace ut_presentacion.Nucleo;

public class EntidadesNucleo
{
    
    public static Almacenes? Almacenes()
    {
        var entidad = new Almacenes();
        entidad.Ubicacion_bodega = "AVENIDA COLOMBIA #398 MEDELLIN ANT" ;
        entidad.Capacidad_bodega = 666.00m;
        return entidad;
    }
    
    public static Miembros? Miembros()
    {
        var entidad = new Miembros();
        entidad.Nombre = "Santiago Hernandez";
        entidad.Fecha_registro = new DateTime(2004, 9, 28, 12, 36, 0);
        entidad.Nivel = "Oro";
        entidad.Puntos = 6800;
        return entidad;
    }

    public static Empleados? Empleados()
    {
        var entidad = new Empleados();
        entidad.Nombre_empleado = "Juan Pérez";
        entidad.Cargo_empleado = "Desarrollador";
        entidad.Sueldo = 5000.00m;
        entidad.Fecha_contratacion = new DateTime(2020, 1, 15);
        return entidad;
    }

    public static Envios? Envios()
    {
        var entidad = new Envios();
        entidad.Estado = "En tránsito";
        entidad.Direccion = "Calle Falsa 123";
        entidad.Transportadora = "Transportes XYZ";
        entidad.empleados = 2;
        return entidad;
    }

    public static Reservas? Reservas()
    {
        var entidad = new Reservas();
            entidad.Fecha_Reserva = DateTime.Now;
            entidad.Estado = "Confirmada";
            entidad.MiembroId = 3;
            entidad.PeliculaId = 2;
            entidad.ConsolaId = 5;
            entidad.EmpleadoId = 4;
        return entidad;
    }

    public static Peliculas? Peliculas()
    {
        var entidad = new Peliculas();
        entidad.Nombre_pelicula = "La Gran Aventura";
        entidad.Genero_Pelicula = "Aventura";
        entidad.Fecha_Estreno = new DateTime(2022, 5, 20);
        entidad.Estado_pelicula = true;
        return entidad;
    }

    public static Consolas? Consolas()
    {
        var entidad = new Consolas();
        entidad.Tipo_consola = "Videojuegos";
        entidad.Marca_consola = "Sony";
        entidad.Estado_consola = 4;
        entidad.almacen = 7;
        return entidad;
    }

    public static Snacks? Snacks()
    {
        var entidad = new Snacks();
        entidad.Nombre = "Chips";
        entidad.Precio = 1.50m;
        entidad.Stock = 100;
        return entidad;
    }

    public static Reservas_Snacks? Reservas_Snacks()
    {
        var entidad = new Reservas_Snacks();
        entidad.SnackId = 4;
        entidad.Reserva = 3;
        return entidad;
    }
}