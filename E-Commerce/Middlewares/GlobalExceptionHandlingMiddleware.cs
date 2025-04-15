using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next,ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundEndPointException(httpContext);
            }
            catch(Exception exception)
            {
                _logger.LogError($"Something Went Wrong{exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleNotFoundEndPointException(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode=(int) HttpStatusCode.NotFound,
                ErrorMessage=$"The EndPoint{httpContext.Request.Path} NotFound"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        //Handle Exceptions
        public async Task HandleExceptionAsync(HttpContext httpContext,Exception exception)
        {
            //Set Status Code To 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //Set Content Type "application/json"
            httpContext.Response.ContentType = "application/json";
            //C#9
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _=>(int) HttpStatusCode.InternalServerError
            };
            //return Standard Response
            var response = new ErrorDetails
            {
                StatusCode=httpContext.Response.StatusCode,
                ErrorMessage=exception.Message
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }
    }
}
