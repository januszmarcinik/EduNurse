using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Api.Configuration
{
    internal static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(
            this IServiceCollection services, 
            IConfiguration configuration,
            IHostingEnvironment environment
            )
        {
            //if (environment.IsEnvironment("Testing"))
            //{
            //    services.AddDbContext<ExamsContext>(options =>
            //    {
            //        options.UseInMemoryDatabase("Testing");
            //    });
            //}
            //else
            //{
            //    services.AddDbContext<ExamsContext>(options =>
            //    {
            //        options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            //    });
            //}
        }
    }
}
