using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces
{
    public interface IRolPermisoAplicacion
    {
        List<RolPermiso> Listar();

        // Buscar por clave compuesta
        RolPermiso? BuscarPorIds(int rolId, int permisoId);

        // Guardar un nuevo RolPermiso
        RolPermiso Guardar(RolPermiso entidad);

        // Modificar un RolPermiso, si aplica (a veces tablas intermedias no requieren modificar)
        RolPermiso Modificar(RolPermiso entidad);

        // Borrar usando clave compuesta
        void Borrar(int rolId, int permisoId);

        // Opcional: listar permisos de un rol específico
        List<RolPermiso> ListarPorRol(int rolId);
    }
}