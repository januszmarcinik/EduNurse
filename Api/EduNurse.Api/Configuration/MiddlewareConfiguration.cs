using EduNurse.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace EduNurse.Api.Configuration
{
    internal static class MiddlewareConfiguration
    {
        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
        }
    }
}
