using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using lib_presentaciones;
using Newtonsoft.Json; // Para JsonConvert.SerializeObject

[TestClass]
public class AlmacenesPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private AlmacenesPresentacion presentacion = null!;
    private Almacenes entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new AlmacenesPresentacion(mockComunicaciones.Object);

        // Inicializa la entidad antes de cada prueba
        entidad = new Almacenes { Id = 1, Ubicacion = "Depósito A", Capacidad = 777 };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Almacenes> { entidad };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista } // PASAR OBJETO (no JSON string)
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Almacenes/Listar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Depósito A", resultado[0].Ubicacion);
    }

    [TestMethod]
    public async Task PorUbicacion_OK()
    {
        var lista = new List<Almacenes> { new Almacenes { Id = 2, Ubicacion = "Zona Norte" } };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Almacenes/PorUbicacion"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorUbicacion(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Zona Norte", resultado[0].Ubicacion);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Almacenes { Id = 0, Ubicacion = "Nuevo" };
        var respuesta = new Dictionary<string, object> {
            { "Entidad", new Almacenes { Id = 5, Ubicacion = "Nuevo" } }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Almacenes/Guardar"))
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

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Almacenes/Modificar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("Depósito A", resultado!.Ubicacion);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones.Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Almacenes/Borrar"))
                          .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones.Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
                          .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
    }
}
