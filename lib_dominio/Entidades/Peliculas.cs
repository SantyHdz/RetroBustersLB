using System;
using System.ComponentModel.DataAnnotations;

namespace lib_dominio.Entidades
{
    public class Peliculas
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public DateTime Fecha_estreno { get; set; }
        public bool Estado { get; set; }
        public int Cantidad { get; set; } // Agregado Recientemente
        public decimal Precio_unitario { get; set; } // Agregado Recientemente
        public decimal Total => Cantidad * Precio_unitario;
    }
}