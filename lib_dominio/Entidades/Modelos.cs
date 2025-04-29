namespace lib_dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Almacenes
{
    [Key]
    public int Id { get; set; }
    public string? Ubicacion_bodega { get; set; }
    public decimal Capacidad_bodega { get; set; }
}

public class Miembros
{
    [Key]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public DateTime Fecha_registro { get; set; }
    public string? Nivel { get; set; }
    public int Puntos { get; set; }
}

public class Empleados
{
    [Key]
    public int Id { get; set; }
    public string? Nombre_empleado { get; set; }
    public string? Cargo_empleado { get; set; }
    public decimal Sueldo { get; set; } // Cambiado a decimal
    public DateTime Fecha_contratacion { get; set; }
}

public class Envios
{
    [Key]
    public int Id { get; set; }
    public string? Estado { get; set; }
    public string? Direccion { get; set; }
    public string? Transportadora { get; set; }
    public int Empleado { get; set; }
    [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }


}

public class Reservas
{
    [Key]
    public int Id { get; set; }
    public DateTime Fecha_Reserva { get; set; }
    public string? Estado { get; set; }
    public int Duracion_reserva { get; set; }
    public decimal? Total_reserva { get; set; }
    public int Miembro { get; set; }
    public int? Pelicula { get; set; }
    public int? Consola { get; set; }
    public int Empleado { get; set; }
    [ForeignKey("Miembro")] public Miembros? _Miembro { get; set; }

    [ForeignKey("Pelicula")] public Peliculas? _Pelicula { get; set; }

    [ForeignKey("Consola")] public Consolas? _Consola { get; set; }
    
    [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
}

public class Peliculas
{
    [Key]
    public int Id { get; set; }
    public string? Nombre_pelicula { get; set; }
    public string? Genero_Pelicula { get; set; }
    public DateTime Fecha_Estreno { get; set; }
    public bool Estado_pelicula { get; set; }
    public int Cantidad_pelis { get; set; } //Agregado Recientemente
    public decimal Precio_unitario { get; set; } //Agregado Recientemente
    public decimal? Total { get; set; } //Agregado Recientemente
}

public class Consolas
{
    [Key]
    public int Id { get; set; }
    public string? Tipo_consola { get; set; }
    public string? Marca_consola { get; set; }
    public int Estado_consola { get; set; }
    public string? Estado_string { get; set; }
    public int Cantidad_consolas { get; set; } //Agregado Recientemente
    public decimal Precio_unitario { get; set; } //Agregado Recientemente
    public decimal? Total { get; set; } //Agregado Recientemente
    public int Almacen { get; set; }
    [ForeignKey("Almacen")] public Almacenes? _Almacen { get; set; }
}

public class Snacks
{
    [Key]
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}

public class Reservas_Snacks
{
    [Key]
    public int Id { get; set; }
    
    public int Cantidad { get; set; }
    public decimal? Total { get; set; }
    public int Snack { get; set; }
    public int Reserva { get; set; }
    [ForeignKey("Snack")] public Snacks? _Snack { get; set; }
    [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }
}