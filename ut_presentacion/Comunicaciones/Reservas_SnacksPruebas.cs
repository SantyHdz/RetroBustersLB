using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class Reservas_SnacksPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private Reservas_SnacksPresentacion presentacion = null!;
    private Reservas_Snacks entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new Reservas_SnacksPresentacion(mockComunicaciones.Object);

        entidad = new Reservas_Snacks { Id = 1, Cantidad = 5, Snack = 2, Reserva = 3 };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Reservas_Snacks> { entidad };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas_Snacks/Listar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual(5, resultado[0].Cantidad);
    }

    [TestMethod]
    public async Task PorCantidad_OK()
    {
        var lista = new List<Reservas_Snacks> { new Reservas_Snacks { Id = 2, Cantidad = 10, Snack = 1, Reserva = 1 } };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas_Snacks/PorCantidad"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorCantidad(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual(10, resultado[0].Cantidad);
    }

    [TestMethod]
    public async Task PorSnacks_OK()
    {
        var lista = new List<Reservas_Snacks> { new Reservas_Snacks { Id = 3, Cantidad = 7, Snack = 2, Reserva = 1 } };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas_Snacks/PorSnacks"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorSnacks(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual(2, resultado[0].Snack);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Reservas_Snacks { Id = 0, Cantidad = 3, Snack = 1, Reserva = 1 };
        var respuesta = new Dictionary<string, object> {
            { "Entidad", new Reservas_Snacks { Id = 5, Cantidad = 3, Snack = 1, Reserva = 1 } }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas_Snacks/Guardar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(5, resultado!.Id);
    }

    [TestMethod]
    public async Task Guardar_ConIdNoCero_LanzaExcepcion()
    {
        var entidadConId = new Reservas_Snacks { Id = 3, Cantidad = 1 };

        await Assert.ThrowsExceptionAsync<System.Exception>(async () =>
        {
            await presentacion.Guardar(entidadConId);
        });
    }

    [TestMethod]
    public async Task Modificar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas_Snacks/Modificar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(5, resultado!.Cantidad);
    }

    [TestMethod]
    public async Task Modificar_ConIdCero_LanzaExcepcion()
    {
        var entidadSinId = new Reservas_Snacks { Id = 0, Cantidad = 5 };

        await Assert.ThrowsExceptionAsync<System.Exception>(async () =>
        {
            await presentacion.Modificar(entidadSinId);
        });
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Reservas_Snacks/Borrar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
    }

    [TestMethod]
    public async Task Borrar_ConIdCero_LanzaExcepcion()
    {
        var entidadSinId = new Reservas_Snacks { Id = 0 };

        await Assert.ThrowsExceptionAsync<System.Exception>(async () =>
        {
            await presentacion.Borrar(entidadSinId);
        });
    }
}
