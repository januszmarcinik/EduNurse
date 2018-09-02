using System.Reflection;
using EduNurse.Api.Shared;
using EduNurse.Exams.AzureTableStorage;

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
                .SetupMapper(config => { config.AddProfile<ExamsMappings>(); })
                .SetupCommandHandlers(assembly)
                .SetupQueryHandlers(assembly)
                .SubscribeToSettings<ExamsSettings>("Exams")
                .RegisterScoped<AtsExamsContext>()
                .RegisterScoped<IExamsRepository, AtsExamsRepository>();
        }
    }
}
