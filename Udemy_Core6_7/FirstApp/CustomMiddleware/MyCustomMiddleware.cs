using System.Runtime.CompilerServices;

namespace FirstApp.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("\n my custom middleware- Starts");
            await next(context);
            await context.Response.WriteAsync("\n my custom middleware- Ends");
        }
    }

    //extension method
    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
