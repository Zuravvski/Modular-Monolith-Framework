namespace Zuravvski.Infrastructure.Integration
{
    public interface IIntegrationEventBusSubscriber
    {
        public IIntegrationEventBusSubscriber Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;
    }
}
