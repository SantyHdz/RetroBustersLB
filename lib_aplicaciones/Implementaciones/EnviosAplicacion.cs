using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class EnviosAplicacion : IEnviosAplicacion
{
    private IConexion? IConexion = null;

    public EnviosAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Envios? Guardar(Envios? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Envios!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Envios? Modificar(Envios? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Envios>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Envios? Borrar(Envios? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Envios!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Envios> Listar()
    {
        return this.IConexion!.Envios!.Take(20).ToList();
    }

    public List<Envios> PorEstado(Envios? entidad)
    {
        return this.IConexion!.Envios!
            .Where(x => x.Estado!.Contains(entidad!.Estado!))
            .ToList();
    }
}
