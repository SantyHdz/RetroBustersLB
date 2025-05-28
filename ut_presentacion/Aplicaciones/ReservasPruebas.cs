using lib_aplicaciones.Implementaciones; 
using lib_aplicaciones.Interfaces; 
using lib_dominio.Entidades; 
using lib_repositorios.Implementaciones; 
using lib_repositorios.Interfaces; 
using ut_presentacion.Nucleo; 
 
namespace ut_presentacion.Aplicaciones 
{ 
    [TestClass] 
    public class ReservasPruebas 
    { 
        private readonly IReservasAplicacion? iAplicacion; 
        private readonly IConexion? iConexion; 
        private List<Reservas>? lista; 
        private Reservas? entidad; 
 
        public ReservasPruebas() 
        { 
            iConexion = new ConexionEF3.Conexion(); 
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion"); 
            iAplicacion = new ReservasAplicacion(iConexion); 
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
            this.lista = this.iAplicacion!.Listar(); 
            return lista.Count > 0; 
        }

        public bool Guardar()
        {
            var miembros = this.iConexion!.Miembros!.FirstOrDefault(x => x.Id == 2);
            var peliculas = this.iConexion!.Peliculas!.FirstOrDefault(x => x.Id == 1);
            var consolas = this.iConexion!.Consolas!.FirstOrDefault(x => x.Id == 2);
            var empleados = this.iConexion!.Empleados!.FirstOrDefault(x => x.Id == 3);
            this.entidad = EntidadesNucleo.Reservas(miembros, peliculas, consolas, empleados, iConexion)!;

        this.iAplicacion!.Guardar(this.entidad); 
            return true; 
        } 
 
        public bool Modificar() 
        { 
            this.iAplicacion!.Modificar(this.entidad); 
            return true; 
        } 
 
        public bool Borrar() 
        { 
            this.iAplicacion!.Borrar(this.entidad); 
            return true; 
        } 
    } 
}