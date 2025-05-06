using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Consolas
    {
        [Key]
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? Marca { get; set; }
        public int Estado { get; set; }
        public string? Estado_string { get; set; }
        public int Cantidad { get; set; } //Agregado Recientemente
        public decimal Precio_unitario { get; set; } //Agregado Recientemente
        public decimal Total => Cantidad * Precio_unitario;
        public int Almacen { get; set; }
        [ForeignKey("Almacen")] public Almacenes? _Almacen { get; set; }
    }
}
