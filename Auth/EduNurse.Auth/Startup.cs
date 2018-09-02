using System.Reflection;
using EduNurse.Api.Shared;
using EduNurse.Auth.AzureTableStorage;
using EduNurse.Auth.Services;

namespace EduNurse.Auth
{
    public static class Startup
    {
        public static void Configure(IFactoryBuilder factoryBuilder)
        {
            var assembly = typeof(Startup)
                .GetTypeInfo()
                .Assembly;

            factoryBuilder
                .SetupMapper(config => { config.AddProfile<AuthMappings>(); })
                .SetupCommandHandlers(assembly)
                .SubscribeToSettings<Settings>("Auth")
                .RegisterScoped<AtsUsersContext>()
                .RegisterScoped<IPasswordService, PasswordService>()
                .RegisterScoped<IUsersRepository, AtsUsersRepository>();
        }
    }
}
