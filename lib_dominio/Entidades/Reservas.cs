using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Reservas
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha_Reserva { get; set; }
        public string? Estado { get; set; }
        public int Duracion { get; set; }
        public decimal? Total { get; set; }
        public int Miembro { get; set; }
        public int? Pelicula { get; set; }
        public int? Consola { get; set; }
        public int Empleado { get; set; }
        [ForeignKey("Miembro")] public Miembros? _Miembro { get; set; }

        [ForeignKey("Pelicula")] public Peliculas? _Pelicula { get; set; }

        [ForeignKey("Consola")] public Consolas? _Consola { get; set; }

        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
    }
}
