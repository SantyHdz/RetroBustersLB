using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_repositorios.Interfaces;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriaAplicacion : IAuditoriaAplicacion
    {
        private IConexion? IConexion = null;

        public AuditoriaAplicacion(IConexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.StringConexion = StringConexion;
        }

        public List<Auditoria> Listar()
        {
            return this.IConexion!.Auditorias!.Take(200).ToList();
        }

        public Auditoria? ObtenerPorId(int id)
        {
            return this.IConexion!.Auditorias!.FirstOrDefault(a => a.Id == id);
        }
    }
}