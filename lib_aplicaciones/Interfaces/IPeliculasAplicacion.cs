using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IPeliculasAplicacion
{
    void Configurar(string StringConexion);
    List<Peliculas> Listar();
    Peliculas? Guardar(Peliculas? entidad);
    Peliculas? Modificar(Peliculas? entidad);
    Peliculas? Borrar(Peliculas? entidad);
    List<Peliculas> PorNombre(Peliculas? entidad);
}
