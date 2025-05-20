using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace lib_dominio.Implementaciones
{
    public class PermisosImplementacion : IPermisosAplicacion
    {
        private string _connectionString = "server=SANTY\\SQLEXPRESS;database=RetroBusters;Integrated Security=True;TrustServerCertificate=true;";

        public List<Permisos> Listar()
        {
            var lista = new List<Permisos>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Permisos", conexion);
            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Permisos
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return lista;
        }

        public Permisos? BuscarPorId(int id)
        {
            Permisos? entidad = null;

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Permisos WHERE Id = @id", conexion);
            comando.Parameters.AddWithValue("@id", id);

            var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                entidad = new Permisos
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!
                };
            }

            return entidad;
        }

        public Permisos Guardar(Permisos entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(@"
                INSERT INTO Permisos (Nombre) VALUES (@nombre);
                SELECT SCOPE_IDENTITY();", conexion);

            comando.Parameters.AddWithValue("@nombre", entidad.Nombre);

            var idNuevo = Convert.ToInt32(comando.ExecuteScalar()!);
            entidad.Id = idNuevo;

            return entidad;
        }

        public Permisos Modificar(Permisos entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(@"
                UPDATE Permisos SET Nombre = @nombre WHERE Id = @id", conexion);

            comando.Parameters.AddWithValue("@id", entidad.Id);
            comando.Parameters.AddWithValue("@nombre", entidad.Nombre);

            comando.ExecuteNonQuery();

            return entidad;
        }

        public Permisos Borrar(Permisos entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("DELETE FROM Permisos WHERE Id = @id", conexion);
            comando.Parameters.AddWithValue("@id", entidad.Id);

            comando.ExecuteNonQuery();

            return entidad;
        }
    }
}
