using Autofac;

namespace Zuravvski.Infrastructure.Integration.EventProcessor.Autofac
{
    public static class Extensions
    {

        public static void UseEventProcessor<TEventMapper>(this ContainerBuilder builder)
            where TEventMapper : class, IEventMapper
        {
            builder.RegisterType<TEventMapper>()
                .As<IEventMapper>()
                .SingleInstance();

            builder.RegisterType<EventProcessor>()
                .As<IEventProcessor>()
                .InstancePerLifetimeScope();
        }
    }
}
