using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Almacenes
    {
        [Key]
        public int Id { get; set; }
        public string? Ubicacion { get; set; }
        public decimal Capacidad { get; set; }
    }
}