using lib_dominio.Entidades;
using lib_repositorios.Interfaces;

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

    public static Envios? Envios(Empleados empleados)
    {
        var entidad = new Envios();
        entidad.Estado = "En tránsito";
        entidad.Direccion = "Calle Falsa 123";
        entidad.Transportadora = "Transportes XYZ";
        entidad.Empleado = empleados.Id;
        return entidad;
    }

    public static Reservas? Reservas(Miembros miembro, Peliculas? peliculas, Consolas? consolas, Empleados empleados, IConexion iConexion)
    {
        var entidad = new Reservas();
            entidad.Fecha_Reserva = DateTime.Now;
            entidad.Estado = "Confirmada";
            entidad.Duracion_reserva = 3; //Duracion En dias de una reserva
            entidad.Total_reserva = (peliculas.Total + consolas.Total + ObtenerValorSnacks(iConexion, 4)) * entidad.Duracion_reserva;
            entidad.Miembro = miembro.Id;
            entidad.Pelicula = peliculas.Id;
            entidad.Consola = consolas.Id;
            entidad.Empleado = empleados.Id;
        return entidad;
    }

    public static Peliculas? Peliculas()
    {
        var entidad = new Peliculas();
        entidad.Nombre_pelicula = "Toy Story 4";
        entidad.Genero_Pelicula = "Animacion";
        entidad.Fecha_Estreno = new DateTime(2022, 5, 20);
        entidad.Estado_pelicula = true;
        entidad.Cantidad_pelis = 3;
        entidad.Precio_unitario = 17000.00m;
        entidad.Total = entidad.Cantidad_pelis * entidad.Precio_unitario;
        return entidad;
    }

    public static Consolas? Consolas(Almacenes almacen)
    {
        var entidad = new Consolas();
        entidad.Tipo_consola = "Videojuegos";
        entidad.Marca_consola = "Sony";
        entidad.Estado_consola = 4;
        entidad.Cantidad_consolas = 2;
        entidad.Precio_unitario = 35000.00m;
        entidad.Total = entidad.Cantidad_consolas * entidad.Precio_unitario;
        entidad.Almacen = almacen.Id;
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

    public static Reservas_Snacks? Reservas_Snacks(Reservas reservas, Snacks snacks)
    {
        var entidad = new Reservas_Snacks();
        entidad.Cantidad = 3;
        entidad.Total = snacks.Precio * entidad.Cantidad;
        entidad.Snack = snacks.Id;
        entidad.Reserva = reservas.Id;
        return entidad;
    }

    public static decimal? ObtenerValorSnacks(IConexion iConexion, int Id_reserva)
    {
        var total = iConexion.Reservas_Snacks
            .Where(x => x.Reserva == Id_reserva)
            .Sum(x => x.Total);

        return total > 0 ? total : (decimal?)null; // Retornar null si no hay snacks
    }
}