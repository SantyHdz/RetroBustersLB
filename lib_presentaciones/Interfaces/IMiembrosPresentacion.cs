using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IMiembrosPresentacion
    {
        Task<List<Miembros>> Listar();
        Task<List<Miembros>> Pornombre(Miembros? entidad);
        Task<Miembros?> Guardar(Miembros? entidad);
        Task<Miembros?> Modificar(Miembros? entidad);
        Task<Miembros?> Borrar(Miembros? entidad);
    }
}