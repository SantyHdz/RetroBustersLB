using lib_dominio.Entidades;

namespace lib_presentaciones.Interfaces
{
    public interface IReservas_SnacksPresentacion
    {
        Task<List<Reservas_Snacks>> Listar();
        Task<List<Reservas_Snacks>> PorCantidad(Reservas_Snacks? entidad);
        Task<List<Reservas_Snacks>> PorSnacks(Reservas_Snacks? entidad);
        Task<Reservas_Snacks?> Guardar(Reservas_Snacks? entidad);
        Task<Reservas_Snacks?> Modificar(Reservas_Snacks? entidad);
        Task<Reservas_Snacks?> Borrar(Reservas_Snacks? entidad);
    }
}