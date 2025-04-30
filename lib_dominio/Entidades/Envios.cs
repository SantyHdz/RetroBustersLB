using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
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
}
