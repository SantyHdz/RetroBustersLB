using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class ReservasAplicacion : IReservasAplicacion
{
    private IConexion? IConexion = null;

    public ReservasAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Reservas? Guardar(Reservas? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Reservas!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Reservas? Modificar(Reservas? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Reservas>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Reservas? Borrar(Reservas? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Reservas!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Reservas> Listar()
    {
        return this.IConexion!.Reservas!.Take(20).ToList();
    }

    public List<Reservas> PorEstado(Reservas? entidad)
    {
        return this.IConexion!.Reservas!
            .Where(x => x.Estado!.Contains(entidad!.Estado!))
            .ToList();
    }
}

