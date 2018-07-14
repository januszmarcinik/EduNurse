using EduNurse.Exams.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace EduNurse.Exams.Api.Configuration
{
    internal static class MiddlewareConfiguration
    {
        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
        }
    }
}
