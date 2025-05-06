using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace lib_repositorios.Implementaciones;

public class ConexionEF3
    {
        private string string_conexion = "server=SANTY\\SQLEXPRESS;database=RetroBusters;Integrated Security=True;TrustServerCertificate=true;";
        // server=localhost;database=db_instrumentos;uid=sa;pwd=Clas3sPrO2024_!;TrustServerCertificate=true;
        // server=localhost;database=db_instrumentos;Integrated Security=True;TrustServerCertificate=true;

        public void ConexionBasica()
        {
            var conexion = new Conexion();
            conexion.StringConexion = this.string_conexion;
        }

        /*public void ConexionInsert()
        {
            var conexion = new Conexion3();
            conexion.StringConnection = this.string_conexion;

            var tipo = conexion.Tipos!.FirstOrDefault(x => x.Nombre == "Viento");

            var instrumento = new Instrumentos();
            instrumento.Codigo = "TS-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            instrumento.Nombre = "Prueba";
            instrumento.Cantidad = 2;
            instrumento.Fecha = DateTime.Now;
            instrumento.Tipo = tipo!.Id;
            instrumento.Activo = false;

            conexion.Instrumentos!.Add(instrumento);
            conexion.SaveChanges();
        }
    }*/

    public partial class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Almacenes>? Almacenes { get; set; }
        public DbSet<Miembros>? Miembros { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Envios>? Envios { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Peliculas>? Peliculas { get; set; }
        public DbSet<Consolas>? Consolas { get; set; }
        public DbSet<Snacks>? Snacks { get; set; }
        public DbSet<Reservas_Snacks>? Reservas_Snacks { get; set; }
        public DbSet<Auditoria>? Auditorias { get; set; }

        public override int SaveChanges()
        {
            RegistrarCambiosAuditables();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RegistrarCambiosAuditables();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void RegistrarCambiosAuditables()
        {
            var entradas = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added 
                            || e.State == EntityState.Modified 
                            || e.State == EntityState.Deleted)
                .ToList();  // Materializar la lista primero

            foreach (var entry in entradas)
            {
                var auditoria = new Auditoria
                {
                    Tabla = entry.Metadata.GetTableName() ?? entry.Entity.GetType().Name,
                    Accion = entry.State.ToString(),
                    Fecha = DateTime.UtcNow,
                    Usuario = ObtenerUsuarioActual(),
                    LlavePrimaria = ObtenerLlavePrimaria(entry),
                    Cambios = ObtenerCambios(entry)
                };

                Auditorias.Add(auditoria);
            }
        }

        private string ObtenerLlavePrimaria(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            var llave = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
            return llave?.CurrentValue?.ToString() ?? "";
        }

        private string ObtenerCambios(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            var cambios = new List<string>();

            if(entry.State == EntityState.Added)
            {
                foreach(var prop in entry.Properties)
                {
                    cambios.Add($"{prop.Metadata.Name} = {prop.CurrentValue}");
                }
            }
            else if (entry.State == EntityState.Deleted)
            {
                foreach(var prop in entry.Properties)
                {
                    cambios.Add($"{prop.Metadata.Name} (original) = {prop .OriginalValue}");
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                foreach (var prop in entry.Properties)
                {
                    if (prop.IsModified)
                    {
                        cambios.Add($"{prop.Metadata.Name}: {prop.OriginalValue} => {prop.CurrentValue}");
                    }
                }
            }

            return string.Join("; ", cambios);
        }

        private string ObtenerUsuarioActual()
        {
            // Implementa la lógica para obtener el usuario actual si es necesario
            return "usuario-sistema"; // Ejemplo placeholder
        }
    }
}