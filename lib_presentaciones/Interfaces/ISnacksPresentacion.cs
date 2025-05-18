using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface ISnacksPresentacion
    {
        Task<List<Snacks>> Listar();
        Task<List<Snacks>> PorNombre(Snacks? entidad);
        Task<Snacks?> Guardar(Snacks? entidad);
        Task<Snacks?> Modificar(Snacks? entidad);
        Task<Snacks?> Borrar(Snacks? entidad);
    }
}