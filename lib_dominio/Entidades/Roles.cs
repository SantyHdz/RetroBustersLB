namespace lib_dominio.Entidades;

public class Roles
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public List<Permisos> Permisos { get; set; } = new();
}
