using Microsoft.Extensions.DependencyInjection;

namespace Zuravvski.Infrastructure.Integration.Memory.DependencyInjection
{
    internal sealed class InMemoryIntegrationEventBusSubscriber : IIntegrationEventBusSubscriber
    {
        private readonly IServiceScopeFactory _serviceProviderFactory;

        public InMemoryIntegrationEventBusSubscriber(IServiceScopeFactory serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory;
        }

        public IIntegrationEventBusSubscriber Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            InMemoryIntegrationEventBus.Instance.Subscribe(() =>
            {
                using var scope = _serviceProviderFactory.CreateScope();
                return scope.ServiceProvider.GetRequiredService<TIntegrationEventHandler>();
            });
            return this;
        }
    }
}
