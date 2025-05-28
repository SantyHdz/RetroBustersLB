using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;

namespace lib_presentaciones.Implementaciones
{
    public class Reservas_SnacksPresentacion : IReservas_SnacksPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        public Reservas_SnacksPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones;
        }

        public async Task<List<Reservas_Snacks>> Listar()
        {
            var datos = comunicaciones.ConstruirUrl(new Dictionary<string, object>(), "Reservas_Snacks/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Reservas_Snacks>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Reservas_Snacks>> PorCantidad(Reservas_Snacks? entidad)
        {
            var datos = new Dictionary<string, object> { ["Entidad"] = entidad! };
            datos = comunicaciones.ConstruirUrl(datos, "Reservas_Snacks/PorCantidad");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Reservas_Snacks>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Reservas_Snacks>> PorSnacks(Reservas_Snacks? entidad)
        {
            var datos = new Dictionary<string, object> { ["Entidad"] = entidad! };
            datos = comunicaciones.ConstruirUrl(datos, "Reservas_Snacks/PorSnacks");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Reservas_Snacks>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Reservas_Snacks?> Guardar(Reservas_Snacks? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Reservas_Snacks/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reservas_Snacks>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Reservas_Snacks?> Modificar(Reservas_Snacks? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Reservas_Snacks/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reservas_Snacks>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Reservas_Snacks?> Borrar(Reservas_Snacks? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object> { ["Entidad"] = entidad };
            datos = comunicaciones.ConstruirUrl(datos, "Reservas_Snacks/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reservas_Snacks>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}