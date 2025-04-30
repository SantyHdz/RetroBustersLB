using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

public interface IEmpleadosAplicacion
{
    void Configurar(string StringConexion);
    List<Empleados> PorCargo(Empleados? entidad);
    List<Empleados> Listar();
    Empleados? Guardar(Empleados? entidad);
    Empleados? Modificar(Empleados? entidad);
    Empleados? Borrar(Empleados? entidad);

}
