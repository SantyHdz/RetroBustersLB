using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IEnviosAplicacion
{
    void Configurar(string StringConexion);
    List<Envios> Listar();
    Envios? Guardar(Envios? entidad);
    Envios? Modificar(Envios? entidad);
    Envios? Borrar(Envios? entidad);
    List<Envios> PorEstado(Envios? entidad);
}
