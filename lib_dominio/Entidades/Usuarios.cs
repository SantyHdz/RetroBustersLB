namespace lib_dominio.Entidades;

public class Usuarios
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Correo { get; set; } = "";
    public string ContrasenaHash { get; set; } = "";
    public string Direccion { get; set; } = "";

    public int RolId { get; set; }
    public Roles? Rol { get; set; }
}
