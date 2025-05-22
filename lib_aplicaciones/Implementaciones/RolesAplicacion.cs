using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class RolesAplicacion : IRolesAplicacion
{
    private IConexion? IConexion = null;

    public RolesAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Roles? Borrar(Roles? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Roles!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Roles? Guardar(Roles? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Roles!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Roles> Listar()
    {
        return this.IConexion!.Roles!.Take(20).ToList();
    }

    public List<Roles> PorId(Roles? entidad)
    {
        return this.IConexion!.Roles!
            .Where(x => x.Id!.ToString().Contains(entidad!.Id.ToString()))
            .ToList();
    }


    public Roles? Modificar(Roles? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Roles>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }
    
}