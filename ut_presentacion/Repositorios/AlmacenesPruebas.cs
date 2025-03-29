using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios;
    
[TestClass]
public class AlmacenesPruebas
{
    private readonly IConexion? iConexion;
    private List<Almacenes>? lista;
    private Almacenes? entidad;
    public AlmacenesPruebas()
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
        this.lista = this.iConexion!.Almacenes!.ToList();
        return lista.Count > 0;
    }
    public bool Guardar()
    {
        this.entidad = EntidadesNucleo.Almacenes()!;
        this.iConexion!.Almacenes!.Add(this.entidad);
        this.iConexion!.SaveChanges();
        return true;
    }
    public bool Modificar()
    {
        this.entidad!.Capacidad_bodega = 777.00m; 
        var entry = this.iConexion!.Entry<Almacenes>(this.entidad);
        entry.State = EntityState.Modified;
        this.iConexion!.SaveChanges();
        return true;
    }
    public bool Borrar()
    {
        this.iConexion!.Almacenes!.Remove(this.entidad!);
        this.iConexion!.SaveChanges();
        return true;
    }
}