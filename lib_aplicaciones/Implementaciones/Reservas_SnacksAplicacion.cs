using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones;

public class Reservas_SnacksAplicacion : IReservas_SnacksAplicacion
{
    private IConexion? IConexion = null;

    public Reservas_SnacksAplicacion(IConexion iConexion)
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
        return this.IConexion!.Reservas_Snacks!
            .Take(20)
            .Include(x => x._Snack)
            .Include(x => x._Reserva)
            .ToList();
    }

    public List<Reservas_Snacks> PorReserva(Reservas_Snacks? entidad)
    {
        return this.IConexion!.Reservas_Snacks!
            .Include(x => x._Snack)
            .Include(x => x._Reserva)
            .Where(x => x.Reserva == entidad!.Reserva)
            .ToList();
    }

    public List<Reservas_Snacks> PorCantidad(Reservas_Snacks? entidad)
    {
        string nombreSnack = entidad?._Snack?.Nombre ?? "";
        int cantidad = entidad?.Cantidad ?? -1;

        return this.IConexion!.Reservas_Snacks!
            .Include(x => x._Snack)
            .Include(x => x._Reserva)
            .Where(x =>
                (cantidad <= -1 || x.Cantidad == cantidad) &&
                (string.IsNullOrEmpty(nombreSnack) || x._Snack!.Nombre.Contains(nombreSnack))
            )
            .ToList();
    }




    public List<Reservas_Snacks> PorSnack(Reservas_Snacks? entidad)
    {
        return this.IConexion!.Reservas_Snacks!
            .Include(x => x._Snack)
            .Include(x => x._Reserva)
            .Where(x => x.Snack == entidad!.Snack)
            .ToList();
    }
}
