using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IReservasSnacksAplicacion
{
    void Configurar(string StringConexion);
    List<Reservas_Snacks> Listar();
    Reservas_Snacks? Guardar(Reservas_Snacks? entidad);
    Reservas_Snacks? Modificar(Reservas_Snacks? entidad);
    Reservas_Snacks? Borrar(Reservas_Snacks? entidad);
    List<Reservas_Snacks> PorReserva(Reservas_Snacks? entidad);
    List<Reservas_Snacks> PorSnack(Reservas_Snacks? entidad);
}
