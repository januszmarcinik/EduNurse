using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Api.Commands;
using EduNurse.Exams.Api.Queries;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Exams.Api.Configuration
{
    internal static class ContainerConfiguration
    {
        public static IContainer ConfigureContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterInstance(AutoMapperConfig.Initialize()).As<IMapper>().SingleInstance();

            var assembly = typeof(Startup)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();
            builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .InstancePerLifetimeScope();

            return builder.Build();
        }

        public static void ConfigureContainer(this IApplicationLifetime appLifetime, IContainer container)
        {
            appLifetime.ApplicationStopped.Register(container.Dispose);
        }
    }
}
