public interface IComunicaciones
{
    Dictionary<string, object> ConstruirUrl(Dictionary<string, object> datos, string ruta);
    Task<Dictionary<string, object>> Execute(Dictionary<string, object> datos);
}