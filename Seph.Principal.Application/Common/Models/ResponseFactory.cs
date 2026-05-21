using System.Net;

namespace Seph.Principal.Application.Common.Models
{
    /*Esta clase se encarga de generar la respuesta del proceso generico, 
     * es decir, es una clase que se encarga de generar la respuesta de un proceso generico, 
     * ya sea un command o un query, y esta clase se encarga de generar la respuesta de ese proceso generico,
     * ya sea una respuesta de exito o una respuesta de error, y esta clase se encarga de generar la respuesta 
     * de ese proceso generico, ya sea una respuesta de exito o una respuesta de error, 
     * y esta clase se encarga de generar la respuesta de ese proceso generico,
     * ya sea una respuesta de exito o una respuesta de error*/
    public static class ResponseFactory
    {
        //metodo generico que se encarga de generar la respuesta de un proceso generico, ya sea un command o un query,
        public static ResponseWrapper<T> Success<T>(T data, string message = "Operación completada correctamente", HttpStatusCode statusCode = HttpStatusCode.OK)
            => new()
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        //metodo generico que se encarga de generar la respuesta de un proceso generico,
        //ya sea un command o un query, en caso de error
        public static ResponseWrapper<T> Failure<T>(string message, HttpStatusCode statusCode)
            => new()
            {
                StatusCode = statusCode,
                Message = message,
                Data = default!
            };
    }

}
