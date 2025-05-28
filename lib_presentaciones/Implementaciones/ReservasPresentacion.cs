using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentaciones.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lib_presentaciones.Implementaciones
{
    public class ReservasPresentacion : IReservasPresentacion
    {
        private readonly IComunicaciones comunicaciones;

        // Inyección de dependencias mediante constructor
        public ReservasPresentacion(IComunicaciones comunicaciones)
        {
            this.comunicaciones = comunicaciones ?? throw new ArgumentNullException(nameof(comunicaciones));
        }

        public async Task<List<Reservas>> Listar()
        {
            var datos = new Dictionary<string, object>();
            datos = comunicaciones.ConstruirUrl(datos, "Reservas/Listar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }

            return JsonConversor.ConvertirAObjeto<List<Reservas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<List<Reservas>> PorEstado(Reservas? entidad)
        {
            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad!
            };

            datos = comunicaciones.ConstruirUrl(datos, "Reservas/PorEstado");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
            {
                throw new Exception(respuesta["Error"].ToString()!);
            }

            return JsonConversor.ConvertirAObjeto<List<Reservas>>(
                JsonConversor.ConvertirAString(respuesta["Entidades"]));
        }

        public async Task<Reservas?> Guardar(Reservas? entidad)
        {
            if (entidad!.Id != 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad
            };

            datos = comunicaciones.ConstruirUrl(datos, "Reservas/Guardar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reservas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Reservas?> Modificar(Reservas? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad
            };

            datos = comunicaciones.ConstruirUrl(datos, "Reservas/Modificar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reservas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }

        public async Task<Reservas?> Borrar(Reservas? entidad)
        {
            if (entidad!.Id == 0)
                throw new Exception("lbFaltaInformacion");

            var datos = new Dictionary<string, object>
            {
                ["Entidad"] = entidad
            };

            datos = comunicaciones.ConstruirUrl(datos, "Reservas/Borrar");
            var respuesta = await comunicaciones.Execute(datos);

            if (respuesta.ContainsKey("Error"))
                throw new Exception(respuesta["Error"].ToString()!);

            return JsonConversor.ConvertirAObjeto<Reservas>(
                JsonConversor.ConvertirAString(respuesta["Entidad"]));
        }
    }
}
