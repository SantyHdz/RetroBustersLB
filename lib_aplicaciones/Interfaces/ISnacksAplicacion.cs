using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface ISnacksAplicacion
{
    void Configurar(string StringConexion);
    List<Snacks> Listar();
    Snacks? Guardar(Snacks? entidad);
    Snacks? Modificar(Snacks? entidad);
    Snacks? Borrar(Snacks? entidad);
    List<Snacks> PorNombre(Snacks? entidad);
}
