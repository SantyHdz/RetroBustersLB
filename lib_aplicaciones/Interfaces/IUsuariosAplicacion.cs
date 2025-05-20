using lib_dominio.Entidades;
namespace lib_aplicaciones.Interfaces;

    public interface IUsuariosAplicacion
    {
        List<Usuarios> Listar();
        Usuarios? BuscarPorId(int id);
        Usuarios? BuscarPorCorreo(string correo);
        Usuarios Guardar(Usuarios entidad);
        Usuarios Modificar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
    }