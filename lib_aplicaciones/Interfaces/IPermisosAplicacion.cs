using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

    public interface IPermisosAplicacion
    {
        List<Permisos> Listar();
        Permisos? BuscarPorId(int id);
        Permisos Guardar(Permisos entidad);
        Permisos Modificar(Permisos entidad);
        Permisos Borrar(Permisos entidad);
    }