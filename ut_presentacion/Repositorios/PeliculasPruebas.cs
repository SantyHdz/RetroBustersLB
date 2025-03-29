using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PeliculasPruebas
    {
        private readonly IConexion? iConexion;
        private List<Peliculas>? lista;
        private Peliculas? entidad;
        public PeliculasPruebas()
        {
            iConexion = new ConexionEF3.Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }
        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }
        public bool Listar()
        {
            this.lista = this.iConexion!.Peliculas!.ToList();
            return lista.Count > 0;
        }
        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Peliculas()!;
            this.iConexion!.Peliculas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }
        public bool Modificar()
        {
            this.entidad!.Genero_Pelicula = "Testeando pruebas unitarias";
            var entry = this.iConexion!.Entry<Peliculas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }
        public bool Borrar()
        {
            this.iConexion!.Peliculas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}