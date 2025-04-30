using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IReservasAplicacion
{
    void Configurar(string StringConexion);
    List<Reservas> PorEstado(Reservas? entidad);
    List<Reservas> Listar();
    Reservas? Guardar(Reservas? entidad);
    Reservas? Modificar(Reservas? entidad);
    Reservas? Borrar(Reservas? entidad);

}