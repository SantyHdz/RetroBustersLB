using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ConsolasPruebas    
    {
        private readonly IConexion? iConexion;
        private List<Consolas>? lista;
        private Consolas? entidad;
        public ConsolasPruebas()
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
            this.lista = this.iConexion!.Consolas!.ToList();
            return lista.Count > 0;
        }
        public bool Guardar()
        {
            this.entidad = EntidadesNucleo.Consolas()!;
            this.iConexion!.Consolas!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }
        public bool Modificar()
        {
            
            switch (this.entidad!.Estado_consola)
            {
                case 1:
                    entidad.Estado_string = "Disponible";
                    break;

                case 2:
                    entidad.Estado_string = "No stock";
                    break;

                case 3:
                    entidad.Estado_string = "En reparacion";
                    break;
                case 4:
                    entidad.Estado_string = "No disponible en la tienda";
                    break;
                default:
                    Console.WriteLine("Ingrese un numero valido");
                    break;
            }
            var entry = this.iConexion!.Entry<Consolas>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }
        public bool Borrar()
        {
            this.iConexion!.Consolas!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}