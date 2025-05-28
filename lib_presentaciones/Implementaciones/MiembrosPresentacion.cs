using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class MiembrosPresentacion : IMiembrosPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public MiembrosPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public async Task<List<Miembros>> Listar()
        {
            var datos = comunicaciones.ConstruirUrl(new Dictionary<string, object>(), "Miembros/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Miembros>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Miembros>> Pornombre(Miembros? entidad)
        {
            var datos = new Dictionary<string, object> { ["Entidad"] = entidad! };
            datos = comunicaciones.ConstruirUrl(datos, "Miembros/Pornombre");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Miembros>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Miembros?> Guardar(Miembros? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Miembros/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Miembros>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Miembros?> Modificar(Miembros? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Miembros/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Miembros>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Miembros?> Borrar(Miembros? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Miembros/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Miembros>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
