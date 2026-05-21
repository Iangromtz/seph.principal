using System.Net;

namespace Seph.Principal.Application.Common.Models
{
    /*Esta clase se encarga de generar la respuetsa del proceso generico*/
    public class ResponseWrapper<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
