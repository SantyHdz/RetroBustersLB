using lib_dominio.Entidades;
using System.Collections.Generic;

namespace lib_aplicaciones.Interfaces
{
    public interface IAuditoriaAplicacion
    {
        void Configurar(string StringConexion);
        List<Auditoria> Listar();
        Auditoria? ObtenerPorId(int id);
    }
}