using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class MiembrosPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private MiembrosPresentacion presentacion = null!;
    private Miembros entidad = null!;

    [TestInitialize]
    public void Setup()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new MiembrosPresentacion(mockComunicaciones.Object);

        entidad = new Miembros
        {
            Id = 1,
            Nombre = "Juan Pérez",
            Fecha_registro = System.DateTime.Now,
            Nivel = "Avanzado",
            Puntos = 1200
        };
    }

    [TestMethod]
    public async Task Listar_ReturnsList()
    {
        var lista = new List<Miembros> { entidad };
        var respuesta = new Dictionary<string, object> { { "Entidades", lista } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Miembros/Listar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });
        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Juan Pérez", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task Pornombre_ReturnsList()
    {
        var lista = new List<Miembros> { new Miembros { Id = 2, Nombre = "Ana" } };
        var respuesta = new Dictionary<string, object> { { "Entidades", lista } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Miembros/Pornombre"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });
        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Pornombre(new Miembros { Nombre = "Ana" });

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Ana", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task Guardar_Success()
    {
        var nuevaEntidad = new Miembros { Id = 0, Nombre = "Nuevo" };
        var respuesta = new Dictionary<string, object> { { "Entidad", new Miembros { Id = 10, Nombre = "Nuevo" } } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Miembros/Guardar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });
        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(10, resultado!.Id);
        Assert.AreEqual("Nuevo", resultado.Nombre);
    }

    [TestMethod]
    public async Task Modificar_Success()
    {
        var respuesta = new Dictionary<string, object> { { "Entidad", entidad } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Miembros/Modificar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });
        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("Juan Pérez", resultado!.Nombre);
        Assert.AreEqual(1, resultado.Id);
    }

    [TestMethod]
    public async Task Borrar_Success()
    {
        var respuesta = new Dictionary<string, object> { { "Entidad", entidad } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Miembros/Borrar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });
        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
        Assert.AreEqual("Juan Pérez", resultado.Nombre);
    }
}
