using lib_aplicaciones.Implementaciones; 
using lib_aplicaciones.Interfaces; 
using lib_dominio.Entidades; 
using lib_repositorios.Implementaciones; 
using lib_repositorios.Interfaces; 
using ut_presentacion.Nucleo; 
 
namespace ut_presentacion.Aplicaciones 
{ 
    [TestClass] 
    public class AlmacenesPrueba 
    { 
        private readonly IAlmacenesAplicacion? iAplicacion; 
        private readonly IConexion? iConexion; 
        private List<Almacenes>? lista; 
        private Almacenes? entidad; 
 
        public AlmacenesPrueba() 
        { 
            iConexion = new ConexionEF3.Conexion(); 
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion"); 
            iAplicacion = new AlmacenesAplicacion(iConexion); 
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
            this.entidad = EntidadesNucleo.Almacenes()!; 
 
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