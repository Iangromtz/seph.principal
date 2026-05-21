using System.Net;

namespace Seph.Principal.Application.Common.Models
{
    public static class ResponseFactory
    {
        public static ResponseWrapper<T> Success<T>(T data, string message = "Operación completada correctamente", HttpStatusCode statusCode = HttpStatusCode.OK)
            => new()
            {
                StatusCode = statusCode,
                Message = message,
                Data = data
            };

        public static ResponseWrapper<T> Failure<T>(string message, HttpStatusCode statusCode)
            => new()
            {
                StatusCode = statusCode,
                Message = message,
                Data = default!
            };
    }

}
