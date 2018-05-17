using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Higgs.Server
{
    // https://stackoverflow.com/a/38935583/563532
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = (int)HttpStatusCode.InternalServerError;
            if (exception is HttpStatusException requestException)
                code = (int)requestException.StatusCode;

            var result = JsonConvert.SerializeObject(new ErrorWrapper { Error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }

    public class ErrorWrapper 
    {
        public string Error { get;set; }
    }

    public class HttpStatusException : Exception
    {
        public HttpStatusException(HttpStatusCode statusCode, string message = null)
            : base(message)
        {
            StatusCode = statusCode;
        }
        public HttpStatusException(int statusCode, string message = null)
            : this((HttpStatusCode)statusCode, message)
        {
            
        }

        public HttpStatusCode StatusCode { get; }
    }
}
