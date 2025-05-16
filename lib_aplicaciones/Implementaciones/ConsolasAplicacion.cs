using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class ConsolasAplicacion : IConsolasAplicacion
{
    private IConexion? IConexion = null;

    public ConsolasAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Consolas? Guardar(Consolas? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Consolas!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Consolas? Modificar(Consolas? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Consolas>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Consolas? Borrar(Consolas? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Consolas!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Consolas> Listar()
    {
        return this.IConexion!.Consolas!
            .Take(20)
            .Include(x => x._Almacen)
            .ToList();
    }

    public List<Consolas> PorTipo(Consolas? entidad)
    {
        return this.IConexion!.Consolas!
            .Where(x => x.Tipo!.Contains(entidad!.Tipo!))
            .ToList();
    }
}
