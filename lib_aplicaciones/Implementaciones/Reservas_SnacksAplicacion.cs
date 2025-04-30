using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class ReservasSnacksAplicacion : IReservasSnacksAplicacion
{
    private IConexion? IConexion = null;

    public ReservasSnacksAplicacion(IConexion iConexion)
    {
        this.IConexion = iConexion;
    }

    public void Configurar(string StringConexion)
    {
        this.IConexion!.StringConexion = StringConexion;
    }

    public Reservas_Snacks? Guardar(Reservas_Snacks? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id != 0) throw new Exception("lbYaSeGuardo");
        this.IConexion!.Reservas_Snacks!.Add(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Reservas_Snacks? Modificar(Reservas_Snacks? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        var entry = this.IConexion!.Entry<Reservas_Snacks>(entidad);
        entry.State = EntityState.Modified;
        this.IConexion.SaveChanges();
        return entidad;
    }

    public Reservas_Snacks? Borrar(Reservas_Snacks? entidad)
    {
        if (entidad == null) throw new Exception("lbFaltaInformacion");
        if (entidad.Id == 0) throw new Exception("lbNoSeGuardo");
        this.IConexion!.Reservas_Snacks!.Remove(entidad);
        this.IConexion.SaveChanges();
        return entidad;
    }

    public List<Reservas_Snacks> Listar()
    {
        return this.IConexion!.Reservas_Snacks!.Take(20).ToList();
    }

    public List<Reservas_Snacks> PorReserva(Reservas_Snacks? entidad)
    {
        return this.IConexion!.Reservas_Snacks!
            .Where(x => x.Reserva == entidad!.Reserva)
            .ToList();
    }

    public List<Reservas_Snacks> PorSnack(Reservas_Snacks? entidad)
    {
        return this.IConexion!.Reservas_Snacks!
            .Where(x => x.Snack == entidad!.Snack)
            .ToList();
    }
}
