using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace BookStore.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                string message = "[Request] HTTP" + httpContext.Request.Method + "-" + httpContext.Request.Path;
                await _next(httpContext);
                watch.Stop();

                message = "[response] HTTP" + httpContext.Request.Method + "-" + httpContext.Request.Path + " responded "
                    + httpContext.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                Console.WriteLine(message);

            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(httpContext, ex, watch);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception ex, Stopwatch watch)
        {
            string message="Error "+httpContext.Request.Method+"-"+ httpContext.Response.StatusCode+" "+
                ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            Console.WriteLine(message);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result=JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            return httpContext.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {

        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
