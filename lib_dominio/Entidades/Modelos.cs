namespace lib_dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Almacenes
{
    [Key]
    public int Id_bodega { get; set; }
    public string? Ubicacion_bodega { get; set; }
    public decimal Capacidad_bodega { get; set; }
}

public class Miembros
{
    [Key]
    public int Id_miembros { get; set; }
    public string? Nombre { get; set; }
    public DateTime Fecha_registro { get; set; }
    public string? Nivel { get; set; }
    public int Puntos { get; set; }
}

public class Empleados
{
    [Key]
    public int Id_empleados { get; set; }
    public string? Nombre_empleado { get; set; }
    public string? Cargo_empleado { get; set; }
    public decimal Sueldo { get; set; } // Cambiado a decimal
    public DateTime Fecha_contratacion { get; set; }
}

public class Envios
{
    [Key]
    public int Id_envios { get; set; }
    public string? Estado { get; set; }
    public string? Direccion { get; set; }
    public string? Transportadora { get; set; }

    [ForeignKey("Empleados")]
    public int empleados { get; set; }
    public Empleados? Empleados { get; set; }
}

public class Reservas
{
    [Key]
    public int Id_Reserva { get; set; }
    public DateTime Fecha_Reserva { get; set; }
    public string? Estado { get; set; }
    
    [ForeignKey("Id_miembros")]
    public int MiembroId { get; set; }
    public Miembros? Miembro { get; set; }
    
    [ForeignKey("Id_pelicula")]
    public int? PeliculaId { get; set; }
    public Peliculas? Pelicula { get; set; }
    
    [ForeignKey("Id_consola")]
    public int? ConsolaId { get; set; }
    public Consolas? Consola { get; set; }
    
    [ForeignKey("Id_empleados")] 
    public int EmpleadoId { get; set; }
    public Empleados? Empleado { get; set; }
}

public class Peliculas
{
    [Key]
    public int Id_pelicula { get; set; }
    public string? Nombre_pelicula { get; set; }
    public string? Genero_Pelicula { get; set; }
    public DateTime Fecha_Estreno { get; set; }
    public bool Estado_pelicula { get; set; }
}

public class Consolas
{
    [Key]
    public int Id_consola { get; set; }
    public string? Tipo_consola { get; set; }
    public string? Marca_consola { get; set; }
    public int Estado_consola { get; set; }
    public string? Estado_string { get; set; } 
    
    [ForeignKey("_almacen")]
    public int almacen { get; set; }
    public Almacenes? _almacen { get; set; }
}

public class Snacks
{
    [Key]
    public int Id_Snack { get; set; }
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}

public class Reservas_Snacks
{
    [Key]
    public int Id_Reservas_Snacks { get; set; }
    
    [ForeignKey("_Snack")]
    public int SnackId { get; set; }
    public Snacks? _Snack { get; set; }
    
    [ForeignKey("_Reserva")]
    public int Reserva { get; set; }
    public Reservas? _Reserva { get; set; }
}