using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Exams.Api.Configuration
{
    internal static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services)
        {
            services.AddDbContext<ExamsContext>(options =>
            {
                options.UseInMemoryDatabase("Testing");
            });
        }
    }
}
