using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace FirstApp.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // in body: email=admin@example.com&password=admin1234
            if (httpContext.Request.Path == "/" && httpContext.Request.Method == "POST")
            {
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string body = await reader.ReadToEndAsync();
                Dictionary<string, StringValues> cred = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

                if (cred.ContainsKey("email") && cred.ContainsKey("password"))
                {
                    string username = cred["email"];
                    string password = cred["password"];
                    if (username.Equals("admin@example.com") && password.Equals("admin1234"))
                    {
                        httpContext.Response.StatusCode = 200;
                        await httpContext.Response.WriteAsync("Successful login");
                        await _next(httpContext);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid login");
                    }
                }

            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
