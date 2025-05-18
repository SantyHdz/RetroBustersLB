using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IPeliculasPresentacion
    {
        Task<List<Peliculas>> Listar();
        Task<List<Peliculas>> PorNombre(Peliculas? entidad);
        Task<Peliculas?> Guardar(Peliculas? entidad);
        Task<Peliculas?> Modificar(Peliculas? entidad);
        Task<Peliculas?> Borrar(Peliculas? entidad);
    }
}