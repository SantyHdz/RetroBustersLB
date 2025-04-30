using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Miembros
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha_registro { get; set; }
        public string? Nivel { get; set; }
        public int Puntos { get; set; }
    }
}