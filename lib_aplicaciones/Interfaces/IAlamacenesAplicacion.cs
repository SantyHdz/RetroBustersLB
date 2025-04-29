using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;
//hola
public interface IAlmacenesAplicacion
{
    void Configurar(string StringConexion);
    List<Almacenes> PorUbicacion(Almacenes? entidad);
    List<Almacenes> Listar();
    Almacenes? Guardar(Almacenes? entidad);
    Almacenes? Modificar(Almacenes? entidad);
    Almacenes? Borrar(Almacenes? entidad);
}