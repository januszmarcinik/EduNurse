using System;
using System.Reflection;
using AutoMapper;

namespace EduNurse.Api.Shared
{
    public interface IFactoryBuilder
    {
        IFactoryBuilder SetupMapper(Action<IMapperConfigurationExpression> config);
        IFactoryBuilder SetupCommandHandlers(Assembly assembly);
        IFactoryBuilder SetupQueryHandlers(Assembly assembly);
        IFactoryBuilder SubscribeToConnectionString(string name, Action<string, bool> subscription);
        IFactoryBuilder RegisterScoped<TPort, TAdapter>();
        IFactoryBuilder RegisterScoped<TAdapter>();
    }
}
