using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using EduNurse.Exams.Api.Repositories;
using EduNurse.Exams.Shared.Repositories;
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

            builder.RegisterType<ExamsContext>().As<IExamsContext>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionsRepository>().As<IQuestionsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExamsRepository>().As<IExamsRepository>().InstancePerLifetimeScope();
            builder.RegisterInstance(AutoMapperConfig.Initialize()).As<IMapper>().SingleInstance();

            return builder.Build();
        }

        public static void ConfigureContainer(this IApplicationLifetime appLifetime, IContainer container)
        {
            appLifetime.ApplicationStopped.Register(container.Dispose);
        }
    }
}
