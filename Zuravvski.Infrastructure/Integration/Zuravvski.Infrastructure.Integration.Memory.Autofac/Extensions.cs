using System.Reflection;
using Autofac;

namespace Zuravvski.Infrastructure.Integration.Memory
{
    public static class Extensions
    {
        public static void UseInMemoryIntegrationEvents(this ContainerBuilder builder, bool registerHandlersAutomatically = false)
        {
            builder.RegisterType<InMemoryIntegrationEventBusClient>()
                .As<IIntegrationEventBusClient>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InMemoryIntegrationEventBusSubscriber>()
                .As<IIntegrationEventBusSubscriber>()
                .InstancePerLifetimeScope();

            if (registerHandlersAutomatically)
            {
                builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>))
                    .InstancePerLifetimeScope();
            }
        }
    }
}
