using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;
public interface IRolesAplicacion
{
    void Configurar(string StringConexion);
    List<Roles> PorId(Roles? entidad);
    List<Roles> Listar();
    Roles? Guardar(Roles? entidad);
    Roles? Modificar(Roles? entidad);
    Roles? Borrar(Roles? entidad);
}