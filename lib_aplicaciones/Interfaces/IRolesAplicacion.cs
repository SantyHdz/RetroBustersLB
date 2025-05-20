using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

    public interface IRolesAplicacion
    {
        List<Roles> Listar();
        Roles? BuscarPorId(int id);
        Roles Guardar(Roles entidad);
        Roles Modificar(Roles entidad);
        Roles Borrar(Roles entidad);
    }