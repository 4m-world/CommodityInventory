using System;

namespace MyInventoryApp.Api.Services.RequestProvider
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; set; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
