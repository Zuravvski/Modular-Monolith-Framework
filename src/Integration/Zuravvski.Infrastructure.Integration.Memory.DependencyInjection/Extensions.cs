using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zuravvski.Infrastructure.Integration.EventProcessor;

namespace Zuravvski.Infrastructure.Integration.Memory.DependencyInjection
{
    public static class Extensions
    {
        public static void AddInMemoryIntegrationEvents(this IServiceCollection services, bool registerHandlersAutomatically = false)
        {
            services.AddScoped<IIntegrationEventBusClient, InMemoryIntegrationEventBusClient>();
            services.AddScoped<IIntegrationEventBusSubscriber, InMemoryIntegrationEventBusSubscriber>();

            if (registerHandlersAutomatically)
            {
                services.Scan(s => s
                    .FromAssemblies(Assembly.GetCallingAssembly())
                    .AddClasses(types => types.AssignableTo(typeof(IIntegrationEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );
            }
        }

        public static void AddEventProcessor<TEventMapper>(this IServiceCollection services)
            where TEventMapper : class, IEventMapper
        {
            services.AddSingleton<IEventMapper, TEventMapper>();
            services.AddScoped<IEventProcessor, SynchronousEventProcessor>();
        }
    }
}
