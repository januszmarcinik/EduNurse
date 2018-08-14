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
        IFactoryBuilder SubscribeToSettings<T>(string name) where T : class, new();
        IFactoryBuilder RegisterScoped<TPort, TAdapter>() where TAdapter : TPort;
        IFactoryBuilder RegisterScoped<TAdapter>();
    }
}
