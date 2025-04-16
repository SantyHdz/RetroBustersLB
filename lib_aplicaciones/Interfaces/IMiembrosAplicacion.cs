using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IMiemmbrosAplicacion
{
    void Configurar(string StringConexion);
    List<Miembros> Pornombre(Miembros? entidad);
    List<Miembros> Listar();
    Miembros? Guardar(Miembros? entidad);
    Miembros? Modificar(Miembros? entidad);
    Miembros? Borrar(Miembros? entidad);
}