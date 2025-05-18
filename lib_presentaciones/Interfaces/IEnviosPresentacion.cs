using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IEnviosPresentacion
    {
        Task<List<Envios>> Listar();
        Task<List<Envios>> PorEstado(Envios? entidad);
        Task<Envios?> Guardar(Envios? entidad);
        Task<Envios?> Modificar(Envios? entidad);
        Task<Envios?> Borrar(Envios? entidad);
    }
}