namespace lib_dominio.Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Tabla { get; set; } = null!;
        public string Accion { get; set; } = null!; // e.g. Added, Modified, Deleted
        public string LlavePrimaria { get; set; } = null!;
        public string Cambios { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string? Usuario { get; set; } // Opcional: quién hizo el cambio
    }
}