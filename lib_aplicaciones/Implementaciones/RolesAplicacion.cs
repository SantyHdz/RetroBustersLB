using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace lib_dominio.Implementaciones
{
    public class RolesImplementacion : IRolesAplicacion
    {
        private string _connectionString = "server=SANTY\\SQLEXPRESS;database=RetroBusters;Integrated Security=True;TrustServerCertificate=true;";

        public List<Roles> Listar()
        {
            var lista = new List<Roles>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Roles", conexion);
            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Roles
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!
                });
            }

            return lista;
        }

        public Roles? BuscarPorId(int id)
        {
            Roles? entidad = null;

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Roles WHERE Id = @id", conexion);
            comando.Parameters.AddWithValue("@id", id);

            var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                entidad = new Roles
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!
                };
            }

            return entidad;
        }

        public Roles Guardar(Roles entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(@"
                INSERT INTO Roles (Nombre) VALUES (@nombre);
                SELECT SCOPE_IDENTITY();", conexion);

            comando.Parameters.AddWithValue("@nombre", entidad.Nombre);

            var idNuevo = Convert.ToInt32(comando.ExecuteScalar()!);
            entidad.Id = idNuevo;

            return entidad;
        }

        public Roles Modificar(Roles entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(@"
                UPDATE Roles SET Nombre = @nombre WHERE Id = @id", conexion);

            comando.Parameters.AddWithValue("@id", entidad.Id);
            comando.Parameters.AddWithValue("@nombre", entidad.Nombre);

            comando.ExecuteNonQuery();

            return entidad;
        }

        public Roles Borrar(Roles entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("DELETE FROM Roles WHERE Id = @id", conexion);
            comando.Parameters.AddWithValue("@id", entidad.Id);

            comando.ExecuteNonQuery();

            return entidad;
        }
    }
}
