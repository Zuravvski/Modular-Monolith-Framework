using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Integration.Memory.Autofac
{
    internal class InMemoryIntegrationEventBus
    {
        private static readonly Lazy<InMemoryIntegrationEventBus> _instance = new Lazy<InMemoryIntegrationEventBus>(() => new InMemoryIntegrationEventBus());

        private readonly List<IIntegrationEventSubscription> _subscriptions = new List<IIntegrationEventSubscription>();

        public static InMemoryIntegrationEventBus Instance => _instance.Value;

        private InMemoryIntegrationEventBus()
        {
            // Hidden constructor due to singleton design pattern
        }

        public Task Publish<TIntegrationEvent>(params TIntegrationEvent[] events) where TIntegrationEvent : IntegrationEvent
            => Publish(events?.AsEnumerable());

        public Task Publish<TIntegrationEvent>(IEnumerable<TIntegrationEvent> events) where TIntegrationEvent : IntegrationEvent
            => Task.WhenAll(events.Select(@event => Publish(@event)));

        private Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
        {
            var handlers = _subscriptions.Select(subscription => subscription as IntegrationEventSubscription<TIntegrationEvent>)
                .Where(subscription => subscription is { })
                .Select(subscription => subscription.HandlerResolver().Handle(@event));

            return Task.WhenAll(handlers);
        }

        public void Subscribe<TIntegrationEvent>(Func<IIntegrationEventHandler<TIntegrationEvent>> handlerResolver)
            where TIntegrationEvent : IntegrationEvent
        {
            _subscriptions.Add(new IntegrationEventSubscription<TIntegrationEvent>(handlerResolver));
        }
    }
}
