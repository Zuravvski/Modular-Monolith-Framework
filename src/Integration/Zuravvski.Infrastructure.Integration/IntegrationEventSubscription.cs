using System;

namespace Zuravvski.Infrastructure.Integration
{
    public interface IIntegrationEventSubscription
    {
        // Marker interface
    }

    public class IntegrationEventSubscription<TIntegrationEvent> : IIntegrationEventSubscription where TIntegrationEvent : IntegrationEvent
    {
        public Type EventType { get; }
        public Func<IIntegrationEventHandler<TIntegrationEvent>> HandlerResolver { get; }

        public IntegrationEventSubscription(Func<IIntegrationEventHandler<TIntegrationEvent>> handlerResolver)
        {
            EventType = typeof(TIntegrationEvent);
            HandlerResolver = handlerResolver;
        }
    }
}
