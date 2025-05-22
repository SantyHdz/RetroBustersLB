using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAlmacenesPresentacion
    {
        Task<List<Almacenes>> Listar();
        Task<List<Almacenes>> PorUbicacion(Almacenes? entidad);
        Task<Almacenes?> Guardar(Almacenes? entidad);
        Task<Almacenes?> Modificar(Almacenes? entidad);
        Task<Almacenes?> Borrar(Almacenes? entidad);
    }
}