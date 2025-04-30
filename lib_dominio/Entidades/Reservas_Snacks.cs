using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
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
}
