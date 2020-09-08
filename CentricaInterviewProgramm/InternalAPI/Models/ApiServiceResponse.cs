using Newtonsoft.Json;
using System.Net;

namespace InternalAPI.Models
{
    public class ApiServiceResponse<T>
    {
        public HttpStatusCode HttpResponse { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }

        [JsonConstructor]
        public ApiServiceResponse()
        {

        }

        public ApiServiceResponse(HttpStatusCode httpStatusCode)
        {
            HttpResponse = httpStatusCode;
        }

        public ApiServiceResponse(HttpStatusCode httpStatusCode, T content)
        {
            HttpResponse = httpStatusCode;
            Content = content;
        }

        public ApiServiceResponse(HttpStatusCode httpStatusCode, string message)
        {
            HttpResponse = httpStatusCode;
            Message = message;
        }
        public ApiServiceResponse(HttpStatusCode httpStatusCode, string message, T content)
        {
            HttpResponse = httpStatusCode;
            Message = message;
            Content = content;
        }

    }
}
