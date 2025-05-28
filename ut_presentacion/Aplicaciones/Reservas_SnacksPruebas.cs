using lib_aplicaciones.Implementaciones; 
using lib_aplicaciones.Interfaces; 
using lib_dominio.Entidades; 
using lib_repositorios.Implementaciones; 
using lib_repositorios.Interfaces; 
using ut_presentacion.Nucleo; 
 
namespace ut_presentacion.Aplicaciones 
{ 
    [TestClass] 
    public class Reservas_SnacksPruebas 
    { 
        private readonly IReservas_SnacksAplicacion? iAplicacion; 
        private readonly IConexion? iConexion; 
        private List<Reservas_Snacks>? lista; 
        private Reservas_Snacks? entidad; 
 
        public Reservas_SnacksPruebas() 
        { 
            iConexion = new ConexionEF3.Conexion(); 
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion"); 
            iAplicacion = new Reservas_SnacksAplicacion(iConexion); 
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
            var reservas = this.iConexion!.Reservas!.FirstOrDefault(x => x.Id == 2);
            var snacks = this.iConexion!.Snacks!.FirstOrDefault(x => x.Id == 1);
            this.entidad = EntidadesNucleo.Reservas_Snacks(reservas, snacks)!; 
 
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