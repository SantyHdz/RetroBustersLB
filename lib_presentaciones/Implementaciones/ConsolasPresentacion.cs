using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class ConsolasPresentacion : IConsolasPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        // Inyección de dependencias via constructor
        public ConsolasPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public async Task<List<Consolas>> Listar()
        {
            var datos = comunicaciones.ConstruirUrl(new Dictionary<string, object>(), "Consolas/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Consolas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Consolas>> PorTipo(Consolas? entidad)
        {
            var datos = new Dictionary<string, object> { ["Entidad"] = entidad! };
            datos = comunicaciones.ConstruirUrl(datos, "Consolas/PorTipo");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Consolas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Consolas?> Guardar(Consolas? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Consolas/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Consolas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Consolas?> Modificar(Consolas? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Consolas/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Consolas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Consolas?> Borrar(Consolas? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Consolas/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Consolas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
