using System.Reflection;
using Autofac;
using Zuravvski.Infrastructure.Integration.EventProcessor;
using Zuravvski.Infrastructure.Integration.Memory.EventProcessor;

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

        public static void UseEventProcessor<TEventMapper>(this ContainerBuilder builder)
            where TEventMapper : class, IEventMapper
        {
            builder.RegisterType<TEventMapper>()
                .As<IEventMapper>()
                .SingleInstance();

            builder.RegisterType<AutofacBasedEventProcessor>()
                .As<IEventProcessor>()
                .InstancePerLifetimeScope();
        }
    }
}
