using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class ConsolasPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private ConsolasPresentacion presentacion = null!;
    private Consolas entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new ConsolasPresentacion(mockComunicaciones.Object);

        entidad = new Consolas
        {
            Id = 1,
            Tipo = "Portátil",
            Marca = "MarcaX",
            Estado = 1,
            Estado_string = "Nuevo",
            Cantidad = 10,
            Precio_unitario = 100.5m,
            Almacen = 1
        };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Consolas> { entidad };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Consolas/Listar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Portátil", resultado[0].Tipo);
        Assert.AreEqual(100.5m, resultado[0].Precio_unitario);
        Assert.AreEqual(1005m, resultado[0].Total);
    }

    [TestMethod]
    public async Task PorTipo_OK()
    {
        var lista = new List<Consolas>
        {
            new Consolas { Id = 2, Tipo = "Escritorio", Marca = "MarcaY", Cantidad = 5, Precio_unitario = 200m }
        };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Consolas/PorTipo"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorTipo(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Escritorio", resultado[0].Tipo);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Consolas { Id = 0, Tipo = "Portátil", Marca = "MarcaZ", Cantidad = 2, Precio_unitario = 300m };
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", new Consolas { Id = 10, Tipo = "Portátil", Marca = "MarcaZ", Cantidad = 2, Precio_unitario = 300m } }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Consolas/Guardar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(10, resultado!.Id);
    }

    [TestMethod]
    public async Task Modificar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Consolas/Modificar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("MarcaX", resultado!.Marca);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object>
        {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Consolas/Borrar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
    }
}
