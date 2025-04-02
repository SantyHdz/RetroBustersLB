using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ReservasSnacksPruebas
    {
        private readonly IConexion? iConexion;
        private List<Reservas_Snacks>? lista;
        private Reservas_Snacks? entidad;
        public ReservasSnacksPruebas()
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
            this.lista = this.iConexion!.Reservas_Snacks!.ToList();
            return lista.Count > 0;
        }
        public bool Guardar()
        {
            var reservas = this.iConexion!.Reservas!.FirstOrDefault(x => x.Id_Reserva == 2);
            var snacks = this.iConexion!.Snacks!.FirstOrDefault(x => x.Id_Snack == 3);
            this.entidad = EntidadesNucleo.Reservas_Snacks(reservas, snacks)!;
            this.iConexion!.Reservas_Snacks!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }
        public bool Modificar()
        {
            this.entidad!.Id_Reservas_Snacks = entidad!.Id_Reservas_Snacks;
            var entry = this.iConexion!.Entry<Reservas_Snacks>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }
        public bool Borrar()
        {
            this.iConexion!.Reservas_Snacks!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}