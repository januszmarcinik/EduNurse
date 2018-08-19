using System.Reflection;
using EduNurse.Api.Shared;
using EduNurse.Authentication.AzureTableStorage;
using EduNurse.Authentication.Services;

namespace EduNurse.Authentication
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
                .SubscribeToSettings<Settings>("Authentication")
                .RegisterScoped<AtsUsersContext>()
                .RegisterScoped<IPasswordService, PasswordService>()
                .RegisterScoped<IUsersRepository, AtsUsersRepository>();
        }
    }
}
