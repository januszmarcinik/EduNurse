using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using AutoMapper.Configuration;
using EduNurse.Api.Dispatchers;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace EduNurse.Api
{
    internal class FactoryBuilder : IFactoryBuilder
    {
        private readonly ContainerBuilder _builder;
        private readonly IConfiguration _configuration;
        private readonly MapperConfigurationExpression _mapperConfigurationExpression;

        public FactoryBuilder(IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            _builder = new ContainerBuilder();
            _builder.Populate(services);
            _mapperConfigurationExpression = new MapperConfigurationExpression();
        }

        public IFactoryBuilder SetupMapper(Action<IMapperConfigurationExpression> config)
        {
            config(_mapperConfigurationExpression);
            return this;
        }

        public IFactoryBuilder SetupCommandHandlers(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            return this;
        }

        public IFactoryBuilder SetupQueryHandlers(Assembly assembly)
        {
            _builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            return this;
        }

        public IFactoryBuilder SubscribeToSettings<T>(string name) where T : class, new()
        {
            var settings = new T();
            _configuration.GetSection(name).Bind(settings);

            _builder.RegisterInstance(settings).SingleInstance();

            return this;
        }

        public IFactoryBuilder RegisterScoped<TPort, TAdapter>() where TAdapter : TPort
        {
            _builder.RegisterType<TAdapter>().As<TPort>().InstancePerLifetimeScope();
            return this;
        }

        public IFactoryBuilder RegisterScoped<TAdapter>()
        {
            _builder.RegisterType<TAdapter>().AsSelf().InstancePerLifetimeScope();
            return this;
        }

        internal IContainer Build()
        {
            _builder.RegisterInstance(BuildMapper()).As<IMapper>().SingleInstance();

            _builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            _builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .InstancePerLifetimeScope();

            return _builder.Build();
        }

        private IMapper BuildMapper()
        {
            return new MapperConfiguration(_mapperConfigurationExpression).CreateMapper();
        }
    }
}
