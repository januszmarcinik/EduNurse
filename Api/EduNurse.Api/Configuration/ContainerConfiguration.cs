using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Api.Configuration
{
    internal static class ContainerConfiguration
    {
        public static IContainer ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            var factory = new FactoryBuilder(services, configuration);

            Exams.Startup.Configure(factory);
            Auth.Startup.Configure(factory);

            return factory.Build();
        }

        public static void ConfigureContainer(this IApplicationLifetime appLifetime, IContainer container)
        {
            appLifetime.ApplicationStopped.Register(container.Dispose);
        }
    }
}
