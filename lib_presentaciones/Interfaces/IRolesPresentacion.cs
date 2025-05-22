using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IRolesPresentacion
    {
        Task<List<Roles>> Listar();
        Task<List<Roles>> PorId(Roles? entidad);
        Task<Roles?> Guardar(Roles? entidad);
        Task<Roles?> Modificar(Roles? entidad);
        Task<Roles?> Borrar(Roles? entidad);
    }
}