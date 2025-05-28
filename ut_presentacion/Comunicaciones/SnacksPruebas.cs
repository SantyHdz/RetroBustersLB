using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class SnacksPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private SnacksPresentacion presentacion = null!;
    private Snacks entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new SnacksPresentacion(mockComunicaciones.Object);

        entidad = new Snacks { Id = 1, Nombre = "Chips", Precio = 2.5m, Stock = 100 };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Snacks> { entidad };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Snacks/Listar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Chips", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task PorNombre_OK()
    {
        var lista = new List<Snacks> { new Snacks { Id = 2, Nombre = "Galletas", Precio = 1.8m, Stock = 50 } };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Snacks/PorNombre"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var filtro = new Snacks { Nombre = "Galletas" };
        var resultado = await presentacion.PorNombre(filtro);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Galletas", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Snacks { Id = 0, Nombre = "Refresco", Precio = 3.0m, Stock = 200 };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", new Snacks { Id = 5, Nombre = "Refresco", Precio = 3.0m, Stock = 200 } }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Snacks/Guardar"))
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
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Snacks/Modificar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("Chips", resultado!.Nombre);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Snacks/Borrar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
    }
}
