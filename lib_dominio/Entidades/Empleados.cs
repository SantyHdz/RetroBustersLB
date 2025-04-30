using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Empleados
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cargo { get; set; }
        public decimal Sueldo { get; set; } // Cambiado a decimal
        public DateTime Fecha_contratacion { get; set; }
    }
}