using System.Reflection;
using EduNurse.Api.Shared;

namespace EduNurse.Exams
{
    public static class Startup
    {
        public static void Configure(IFactoryBuilder factoryBuilder)
        {
            var assembly = typeof(Startup)
                .GetTypeInfo()
                .Assembly;

            factoryBuilder
                .SetupMapper(config => { config.AddProfile<AutoMapperProfile>(); })
                .SetupCommandHandlers(assembly)
                .SetupQueryHandlers(assembly)
                .SubscribeToConnectionString("MSSQL", Settings.Initialize)
                .RegisterScoped<ExamsContext>()
                .RegisterScoped<IExamsRepository, EfCoreExamsRepository>();
        }
    }
}
