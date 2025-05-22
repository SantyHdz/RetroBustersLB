using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IUsuariosPresentacion
    {
        Task<List<Usuarios>> Listar();
        Task<List<Usuarios>> PorCorreo(Usuarios? entidad);
        Task<Usuarios?> Guardar(Usuarios? entidad);
        Task<Usuarios?> Modificar(Usuarios? entidad);
        Task<Usuarios?> Borrar(Usuarios? entidad);
    }
}