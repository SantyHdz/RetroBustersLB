using lib_dominio.Entidades;
using lib_repositorios.Interfaces;

namespace ut_presentacion.Nucleo;

public class EntidadesNucleo
{
    
    public static Almacenes? Almacenes()
    {
        var entidad = new Almacenes();
        entidad.Ubicacion = "AVENIDA COLOMBIA #398 MEDELLIN ANT" ;
        entidad.Capacidad = 666.00m;
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
        entidad.Nombre = "Juan Pérez";
        entidad.Cargo = "Desarrollador";
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
            entidad.Duracion = 3; //Duracion En dias de una reserva
            entidad.Miembro = miembro.Id;
            entidad.Pelicula = peliculas.Id;
            entidad.Consola = consolas.Id;
            entidad.Empleado = empleados.Id;
        return entidad;
    }

    public static Peliculas? Peliculas()
    {
        var entidad = new Peliculas();
        entidad.Nombre = "Toy Story 4";
        entidad.Genero = "Animacion";
        entidad.Fecha_estreno = new DateTime(2022, 5, 20);
        entidad.Estado = true;
        entidad.Cantidad = 3;
        entidad.Precio_unitario = 17000.00m;
        return entidad;
    }

    public static Consolas? Consolas(Almacenes almacen)
    {
        var entidad = new Consolas();
        entidad.Tipo = "Videojuegos";
        entidad.Marca = "Sony";
        entidad.Estado = 4;
        entidad.Cantidad = 2;
        entidad.Precio_unitario = 35000.00m;
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