using lib_dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace lib_repositorios.Interfaces;

public interface IConexion
{
    string? StringConexion { get; set; }

    DbSet<Almacenes>? Almacenes { get; set; }
    DbSet<Miembros>? Miembros { get; set; }
    DbSet<Empleados>? Empleados { get; set; }
    DbSet<Envios>? Envios { get; set; }
    DbSet<Reservas>? Reservas { get; set; }
    DbSet<Peliculas>? Peliculas { get; set; }
    DbSet<Consolas>? Consolas { get; set; }
    DbSet<Snacks>? Snacks { get; set; }
    DbSet<Reservas_Snacks>? Reservas_Snacks { get; set; }
    DbSet<Auditoria>? Auditorias { get; set; }

    EntityEntry<T> Entry<T>(T entity) where T : class;
    int SaveChanges();
}