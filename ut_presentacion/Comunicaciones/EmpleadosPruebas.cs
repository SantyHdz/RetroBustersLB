using Moq;
using lib_dominio.Entidades;
using lib_presentaciones.Implementaciones;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

[TestClass]
public class EmpleadosPresentacionTests
{
    private Mock<IComunicaciones> mockComunicaciones = null!;
    private EmpleadosPresentacion presentacion = null!;
    private Empleados entidad = null!;

    [TestInitialize]
    public void Init()
    {
        mockComunicaciones = new Mock<IComunicaciones>();
        presentacion = new EmpleadosPresentacion(mockComunicaciones.Object);

        entidad = new Empleados 
        { 
            Id = 1, 
            Nombre = "Juan Perez", 
            Cargo = "Analista", 
            Sueldo = 2500.50m, 
            Fecha_contratacion = new System.DateTime(2020, 1, 15) 
        };
    }

    [TestMethod]
    public async Task Listar_OK()
    {
        var lista = new List<Empleados> { entidad };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista } 
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Empleados/Listar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Listar();

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Juan Perez", resultado[0].Nombre);
        Assert.AreEqual("Analista", resultado[0].Cargo);
    }

    [TestMethod]
    public async Task PorCargo_OK()
    {
        var lista = new List<Empleados> { new Empleados { Id = 2, Nombre = "Ana Gómez", Cargo = "Gerente" } };
        var respuesta = new Dictionary<string, object> {
            { "Entidades", lista }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Empleados/PorCargo"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.PorCargo(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("Gerente", resultado[0].Cargo);
        Assert.AreEqual("Ana Gómez", resultado[0].Nombre);
    }

    [TestMethod]
    public async Task Guardar_OK()
    {
        var nuevaEntidad = new Empleados { Id = 0, Nombre = "Luis Martínez", Cargo = "Programador", Sueldo = 3000m, Fecha_contratacion = System.DateTime.Now };
        var respuesta = new Dictionary<string, object> {
            { "Entidad", new Empleados { Id = 10, Nombre = "Luis Martínez", Cargo = "Programador", Sueldo = 3000m, Fecha_contratacion = nuevaEntidad.Fecha_contratacion } }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Empleados/Guardar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Guardar(nuevaEntidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(10, resultado!.Id);
        Assert.AreEqual("Luis Martínez", resultado.Nombre);
    }

    [TestMethod]
    public async Task Modificar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Empleados/Modificar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Modificar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual("Juan Perez", resultado!.Nombre);
        Assert.AreEqual(1, resultado.Id);
    }

    [TestMethod]
    public async Task Borrar_OK()
    {
        var respuesta = new Dictionary<string, object> {
            { "Entidad", entidad }
        };

        mockComunicaciones
            .Setup(m => m.ConstruirUrl(It.IsAny<Dictionary<string, object>>(), "Empleados/Borrar"))
            .Returns<Dictionary<string, object>, string>((d, u) => { d["url"] = u; return d; });

        mockComunicaciones
            .Setup(m => m.Execute(It.IsAny<Dictionary<string, object>>()))
            .ReturnsAsync(respuesta);

        var resultado = await presentacion.Borrar(entidad);

        Assert.IsNotNull(resultado);
        Assert.AreEqual(1, resultado!.Id);
        Assert.AreEqual("Juan Perez", resultado.Nombre);
    }
}
