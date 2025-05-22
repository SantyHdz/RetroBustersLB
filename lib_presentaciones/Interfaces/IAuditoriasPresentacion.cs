using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IAuditoriasPresentacion
    {
        Task<List<Auditoria>> Listar();
        Task<List<Auditoria>> ObtenerPorId(Auditoria? entidad);
        Task<Auditoria?> Guardar(Auditoria? entidad);
        Task<Auditoria?> Modificar(Auditoria? entidad);
        Task<Auditoria?> Borrar(Auditoria? entidad);
    }
}