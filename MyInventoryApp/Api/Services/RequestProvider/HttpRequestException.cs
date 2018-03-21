using System;
using System.Net;

namespace MyInventoryApp.Api.Services.RequestProvider
{
    public class HttpRequestException :Exception
    {
        public HttpStatusCode HttpCode { get; set; }

        public HttpRequestException(HttpStatusCode code)
            :
        this(code, null, null)
        {
        }

        public HttpRequestException(HttpStatusCode code, string message)
            : this(code, message, null)
        {
        }

        public HttpRequestException(HttpStatusCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            HttpCode = code;
        }
    }
}
