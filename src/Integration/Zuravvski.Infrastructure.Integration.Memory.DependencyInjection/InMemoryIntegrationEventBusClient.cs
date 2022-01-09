namespace Zuravvski.Infrastructure.Integration.Memory.DependencyInjection
{
    internal sealed class InMemoryIntegrationEventBusClient : IIntegrationEventBusClient
    {
        public Task Publish<TIntegrationEvent>(params TIntegrationEvent[] events) where TIntegrationEvent : IntegrationEvent
            => InMemoryIntegrationEventBus.Instance.Publish(events);

        public Task Publish<TIntegrationEvent>(IEnumerable<TIntegrationEvent> events) where TIntegrationEvent : IntegrationEvent
            => InMemoryIntegrationEventBus.Instance.Publish(events);
    }
}
