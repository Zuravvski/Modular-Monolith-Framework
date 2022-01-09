using System.Reflection;
using Autofac;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Commands;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Queries;

namespace Zuravvski.Infrastructure.Cqrs.Memory.Autofac
{
    public static class Extensions
    {
        public static ContainerBuilder AddCqrs(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            return containerBuilder
                .AddCqrsCommands(finalAssembly)
                .AddCqrsQueries(finalAssembly);
        }
        public static ContainerBuilder AddCqrsCommands(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            containerBuilder.RegisterAssemblyTypes(finalAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(finalAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<,>))
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryCommandBus>()
                .As<ICommandBus>()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }

        public static ContainerBuilder AddCqrsQueries(this ContainerBuilder containerBuilder, Assembly assembly = null)
        {
            var finalAssembly = assembly ?? Assembly.GetCallingAssembly();

            containerBuilder.RegisterAssemblyTypes(new[] { finalAssembly })
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<InMemoryQueryBus>()
                .As<IQueryBus>()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }
    }
}
