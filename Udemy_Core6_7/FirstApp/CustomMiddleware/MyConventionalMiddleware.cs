using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FirstApp.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public MyConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query.ContainsKey("name"))
            {
                string name = httpContext.Request.Query["name"];
                await httpContext.Response.WriteAsync($"\n hello {name}");
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyConventionalMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyConventionalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyConventionalMiddleware>();
        }
    }
}
