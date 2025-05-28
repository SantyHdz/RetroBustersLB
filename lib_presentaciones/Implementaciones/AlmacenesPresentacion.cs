using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class AlmacenesPresentacion : IAlmacenesPresentacion
    {
        private readonly IComunicaciones comunicaciones; 

        public AlmacenesPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public async Task<List<Almacenes>> Listar()
        {
            var datos = comunicaciones.ConstruirUrl(new Dictionary<string, object>(), "Almacenes/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Almacenes>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Almacenes>> PorUbicacion(Almacenes? entidad)
        {
            var datos = new Dictionary<string, object> { ["Entidad"] = entidad! };
            datos = comunicaciones.ConstruirUrl(datos, "Almacenes/PorUbicacion");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Almacenes>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Almacenes?> Guardar(Almacenes? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Almacenes/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Almacenes>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Almacenes?> Modificar(Almacenes? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Almacenes/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Almacenes>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Almacenes?> Borrar(Almacenes? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Almacenes/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Almacenes>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }  
}
