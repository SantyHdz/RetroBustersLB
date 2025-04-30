using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class SnacksAplicacion : ISnacksAplicacion
{
    private IConexion? IConexion = null;

    public SnacksAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Snacks? Guardar(Snacks? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Snacks!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Snacks? Modificar(Snacks? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Snacks>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Snacks? Borrar(Snacks? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Snacks!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Snacks> Listar()
    {
        return this.IConexion!.Snacks!.Take(20).ToList();
    }

    public List<Snacks> PorNombre(Snacks? entidad)
    {
        return this.IConexion!.Snacks!
            .Where(x => x.Nombre!.Contains(entidad!.Nombre!))
            .ToList();
    }
}
