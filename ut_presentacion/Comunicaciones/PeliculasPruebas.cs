using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using lib_presentaciones;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class PeliculasPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private PeliculasPresentacion presentacion = null!;
    private Peliculas entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new PeliculasPresentacion(mockComunicaciones.Object);

        entidad = new Peliculas
        {
            Id = 1,
            Nombre = "Pelicula Test",
            Genero = "Accion",
            Fecha_estreno = new System.DateTime(2022, 1, 1),
            Estado = true,
            Cantidad = 10,
            Precio_unitario = 15.5m
        };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Peliculas> { entidad };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Peliculas/Listar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Pelicula Test", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task PorNombre_OK()
    {
        var lista = new List<Peliculas> { new Peliculas { Id = 2, Nombre = "Otra Pelicula" } };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Peliculas/PorNombre"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorNombre(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Otra Pelicula", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Peliculas { Id = 0, Nombre = "Nueva Pelicula" };
        var respuesta = new Dictionary<string, object> {
            { "Entidad", new Peliculas { Id = 5, Nombre = "Nueva Pelicula" } }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Peliculas/Guardar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(5, resultado!.Id);
    }

    [TestMethod]
    public async Task Modificar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Peliculas/Modificar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("Pelicula Test", resultado!.Nombre);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Peliculas/Borrar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
    }
}
