using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib_presentaciones.Implementaciones
{
    public class SnacksPresentacion : ISnacksPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        // Inyección de dependencias mediante constructor
        public SnacksPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones ?? throw new ArgumentNullException(nameof(comunicaciones));
        }

        public async Task<List<Snacks>> Listar()
        {
            var datos = new Dictionary<string, object>();
            datos = comunicaciones.ConstruirUrl(datos, "Snacks/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Snacks>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Snacks>> PorNombre(Snacks? entidad)
        {
            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad!
            };

            datos = comunicaciones.ConstruirUrl(datos, "Snacks/PorNombre");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<List<Snacks>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Snacks?> Guardar(Snacks? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad
            };

            datos = comunicaciones.ConstruirUrl(datos, "Snacks/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Snacks>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Snacks?> Modificar(Snacks? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad
            };

            datos = comunicaciones.ConstruirUrl(datos, "Snacks/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Snacks>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Snacks?> Borrar(Snacks? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad
            };

            datos = comunicaciones.ConstruirUrl(datos, "Snacks/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Snacks>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
