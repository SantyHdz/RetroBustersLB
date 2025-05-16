using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IConsolasPresentacion
    {
        Task<List<Consolas>> Listar();
        Task<List<Consolas>> PorTipo(Consolas? entidad);
        Task<Consolas?> Guardar(Consolas? entidad);
        Task<Consolas?> Modificar(Consolas? entidad);
        Task<Consolas?> Borrar(Consolas? entidad);
    }
}