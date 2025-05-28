using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class EnviosPresentacion : IEnviosPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public EnviosPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public async Task<List<Envios>> Listar()
        {
            var datos = comunicaciones.ConstruirUrl(new Dictionary<string, object>(), "Envios/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Envios>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Envios>> PorEstado(Envios? entidad)
        {
            var datos = new Dictionary<string, object> { ["Entidad"] = entidad! };
            datos = comunicaciones.ConstruirUrl(datos, "Envios/PorEstado");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Envios>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Envios?> Guardar(Envios? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Envios/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Envios>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Envios?> Modificar(Envios? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Envios/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Envios>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Envios?> Borrar(Envios? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Envios/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Envios>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
