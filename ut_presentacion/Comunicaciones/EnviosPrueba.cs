using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class EnviosPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private EnviosPresentacion presentacion = null!;
    private Envios entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new EnviosPresentacion(mockComunicaciones.Object);

        entidad = new Envios
        {
            Id = 1,
            Estado = "En camino",
            Direccion = "Calle Falsa 123",
            Transportadora = "TransExpress",
            Empleado = 42,
            _Empleado = null
        };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Envios> { entidad };
        var respuesta = new Dictionary<string, object> { { "Entidades", lista } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Envios/Listar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("En camino", resultado[0].Estado);
    }

    [TestMethod]
    public async Task PorEstado_OK()
    {
        var lista = new List<Envios> { new Envios { Id = 2, Estado = "Entregado" } };
        var respuesta = new Dictionary<string, object> { { "Entidades", lista } };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Envios/PorEstado"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorEstado(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Entregado", resultado[0].Estado);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Envios { Id = 0, Estado = "Pendiente" };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", new Envios { Id = 10, Estado = "Pendiente" } }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Envios/Guardar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(10, resultado!.Id);
        Assert.AreEqual("Pendiente", resultado.Estado);
    }

    [TestMethod]
    public async Task Modificar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Envios/Modificar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("En camino", resultado!.Estado);
        Assert.AreEqual(1, resultado.Id);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Envios/Borrar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
        Assert.AreEqual("En camino", resultado.Estado);
    }
}
