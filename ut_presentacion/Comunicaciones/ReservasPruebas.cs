using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class ReservasPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private ReservasPresentacion presentacion = null!;
    private Reservas entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new ReservasPresentacion(mockComunicaciones.Object);

        entidad = new Reservas
        {
            Id = 1,
            Fecha_Reserva = System.DateTime.Now,
            Estado = "Pendiente",
            Duracion = 2,
            Miembro = 10,
            Pelicula = 3,
            Consola = 4,
            Empleado = 5
        };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Reservas> { entidad };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas/Listar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Pendiente", resultado[0].Estado);
    }

    [TestMethod]
    public async Task PorEstado_OK()
    {
        var lista = new List<Reservas> { new Reservas { Id = 2, Estado = "Confirmado" } };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas/PorEstado"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorEstado(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Confirmado", resultado[0].Estado);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Reservas
        {
            Id = 0,
            Estado = "Pendiente",
            Fecha_Reserva = System.DateTime.Now,
            Duracion = 1,
            Miembro = 15,
            Pelicula = 1,
            Consola = 2,
            Empleado = 3
        };

        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", new Reservas { Id = 5, Estado = "Pendiente" } }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas/Guardar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(5, resultado!.Id);
    }

    [TestMethod]
    public async Task Modificar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas/Modificar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("Pendiente", resultado!.Estado);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas/Borrar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
    }
}
