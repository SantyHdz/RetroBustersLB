using System.Data.SqlClient;
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;
using Microsoft.Data.SqlClient;

namespace lib_dominio.Implementaciones
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private string _connectionString = "server=SANTY\\SQLEXPRESS;database=RetroBusters;Integrated Security=True;TrustServerCertificate=true;";


        public List<Usuarios> Listar()
        {
            var lista = new List<Usuarios>();

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Usuarios", conexion);
            var reader = comando.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Usuarios
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Correo = reader["Correo"].ToString()!,
                    ContrasenaHash = reader["ContrasenaHash"].ToString()!,
                    Direccion = reader["Direccion"].ToString()!
                });
            }

            return lista;
        }

        public Usuarios? BuscarPorId(int id)
        {
            Usuarios? entidad = null;

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Usuarios WHERE Id = @id", conexion);
            comando.Parameters.AddWithValue("@id", id);

            var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                entidad = new Usuarios
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Correo = reader["Correo"].ToString()!,
                    ContrasenaHash = reader["ContrasenaHash"].ToString()!,
                    Direccion = reader["Direccion"].ToString()!
                };
            }

            return entidad;
        }

        public Usuarios? BuscarPorCorreo(string correo)
        {
            Usuarios? entidad = null;

            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("SELECT * FROM Usuarios WHERE Correo = @correo", conexion);
            comando.Parameters.AddWithValue("@correo", correo);

            var reader = comando.ExecuteReader();

            if (reader.Read())
            {
                entidad = new Usuarios
                {
                    Id = (int)reader["Id"],
                    Nombre = reader["Nombre"].ToString()!,
                    Correo = reader["Correo"].ToString()!,
                    ContrasenaHash = reader["ContrasenaHash"].ToString()!,
                    Direccion = reader["Direccion"].ToString()!
                };
            }

            return entidad;
        }

        public Usuarios Guardar(Usuarios entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(@"
                INSERT INTO Usuarios (Nombre, Correo, ContrasenaHash, Direccion) 
                VALUES (@nombre, @correo, @ContrasenaHash, @direccion);
                SELECT SCOPE_IDENTITY();", conexion);

            comando.Parameters.AddWithValue("@nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@correo", entidad.Correo);
            comando.Parameters.AddWithValue("@ContrasenaHash", entidad.ContrasenaHash);
            comando.Parameters.AddWithValue("@direccion", entidad.Direccion);

            var idNuevo = Convert.ToInt32(comando.ExecuteScalar()!);
            entidad.Id = idNuevo;

            return entidad;
        }

        public Usuarios Modificar(Usuarios entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand(@"
                UPDATE Usuarios SET 
                    Nombre = @nombre,
                    Correo = @correo,
                    ContrasenaHash = @ContrasenaHash,
                    Direccion = @direccion
                WHERE Id = @id", conexion);

            comando.Parameters.AddWithValue("@id", entidad.Id);
            comando.Parameters.AddWithValue("@nombre", entidad.Nombre);
            comando.Parameters.AddWithValue("@correo", entidad.Correo);
            comando.Parameters.AddWithValue("@ContrasenaHash", entidad.ContrasenaHash);
            comando.Parameters.AddWithValue("@direccion", entidad.Direccion);

            comando.ExecuteNonQuery();

            return entidad;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            using var conexion = new SqlConnection(_connectionString);
            conexion.Open();

            var comando = new SqlCommand("DELETE FROM Usuarios WHERE Id = @id", conexion);
            comando.Parameters.AddWithValue("@id", entidad.Id);

            comando.ExecuteNonQuery();

            return entidad;
        }
    }
}
