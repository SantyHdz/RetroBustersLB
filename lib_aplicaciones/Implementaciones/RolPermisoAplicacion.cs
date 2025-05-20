using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace lib_dominio.Implementaciones
{
    public class RolPermisoAplicacion : IRolPermisoAplicacion
    {
        private string _connectionString = "server=SANTY\\SQLEXPRESS;database=RetroBusters;Integrated Security=True;TrustServerCertificate=true;";

        public List<RolPermiso> Listar()
        {
            var lista = new List<RolPermiso>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT RolId, PermisoId FROM RolPermiso", conexion);
            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new RolPermiso
                {
                    RolId = (int)reader["RolId"],
                    PermisoId = (int)reader["PermisoId"]
                });
            }

            return lista;
        }

        public RolPermiso? BuscarPorIds(int rolId, int permisoId)
        {
            RolPermiso? entidad = null;

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(
                "SELECT RolId, PermisoId FROM RolPermiso WHERE RolId = @rolId AND PermisoId = @permisoId", conexion);
            comando.Parameters.AddWithValue("@rolId", rolId);
            comando.Parameters.AddWithValue("@permisoId", permisoId);

            var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                entidad = new RolPermiso
                {
                    RolId = (int)reader["RolId"],
                    PermisoId = (int)reader["PermisoId"]
                };
            }

            return entidad;
        }

        public RolPermiso Guardar(RolPermiso entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(
                "INSERT INTO RolPermiso (RolId, PermisoId) VALUES (@rolId, @permisoId)", conexion);
            comando.Parameters.AddWithValue("@rolId", entidad.RolId);
            comando.Parameters.AddWithValue("@permisoId", entidad.PermisoId);

            comando.ExecuteNonQuery();

            return entidad;
        }

        public RolPermiso Modificar(RolPermiso entidad)
        {
            // Generalmente no se modifican relaciones en tablas intermedias,
            // pero si quieres implementarlo, sería borrar la relación antigua y crear la nueva.
            // Aquí una implementación simple que no hace nada:

            throw new NotImplementedException("Modificar no está implementado para tablas intermedias. " +
                                              "Considera borrar y crear la relación.");
        }

        public void Borrar(int rolId, int permisoId)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(
                "DELETE FROM RolPermiso WHERE RolId = @rolId AND PermisoId = @permisoId", conexion);
            comando.Parameters.AddWithValue("@rolId", rolId);
            comando.Parameters.AddWithValue("@permisoId", permisoId);

            comando.ExecuteNonQuery();
        }

        public List<RolPermiso> ListarPorRol(int rolId)
        {
            var lista = new List<RolPermiso>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(
                "SELECT RolId, PermisoId FROM RolPermiso WHERE RolId = @rolId", conexion);
            comando.Parameters.AddWithValue("@rolId", rolId);

            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new RolPermiso
                {
                    RolId = (int)reader["RolId"],
                    PermisoId = (int)reader["PermisoId"]
                });
            }

            return lista;
        }
    }
}
