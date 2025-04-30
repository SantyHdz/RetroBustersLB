using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IConsolasAplicacion
{
    void Configurar(string StringConexion);
    List<Consolas> PorTipo(Consolas? entidad);
    List<Consolas> Listar();
    Consolas? Guardar(Consolas? entidad);
    Consolas? Modificar(Consolas? entidad);
    Consolas? Borrar(Consolas? entidad);

}
