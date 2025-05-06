using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_dominio.Entidades
{
    public class Reservas_Snacks
    {
        [Key]
        public int Id { get; set; }

        public int Cantidad { get; set; }
        // Cambiar Total a una propiedad calculada
        [NotMapped] // Esto indica que no se debe mapear a la base de datos
        public decimal Total => (_Snack?.Precio ?? 0) * Cantidad; // Calcula el total basado en el precio del snack y la cantidad

        public int Snack { get; set; }
        public int Reserva { get; set; }

        [ForeignKey("Snack")]
        public Snacks? _Snack { get; set; }

        [ForeignKey("Reserva")]
        public Reservas? _Reserva { get; set; }
    }
}
