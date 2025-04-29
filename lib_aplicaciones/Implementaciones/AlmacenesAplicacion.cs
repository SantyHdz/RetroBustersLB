using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class AlmacenesAplicacion : IAlmacenesAplicacion
{
    private IConexion? IConexion = null;

    public AlmacenesAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Almacenes? Borrar(Almacenes? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Almacenes!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Almacenes? Guardar(Almacenes? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Almacenes!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Almacenes> Listar()
    {
        return this.IConexion!.Almacenes!.Take(20).ToList();
    }

    public List<Almacenes> PorUbicacion(Almacenes? entidad)
    {
        return this.IConexion!.Almacenes!
            .Where(x => x.Ubicacion_bodega!.Contains(entidad!.Ubicacion_bodega!))
            .ToList();
    }

    public Almacenes? Modificar(Almacenes? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Almacenes>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }
}
