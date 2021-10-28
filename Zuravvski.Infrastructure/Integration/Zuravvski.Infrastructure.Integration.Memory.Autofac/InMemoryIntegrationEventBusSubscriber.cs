using Autofac;

namespace Zuravvski.Infrastructure.Integration.Memory
{
    internal sealed class InMemoryIntegrationEventBusSubscriber : IIntegrationEventBusSubscriber
    {
        private readonly IComponentContext _context;

        public InMemoryIntegrationEventBusSubscriber(IComponentContext context)
        {
            _context = context;
        }

        public IIntegrationEventBusSubscriber Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            InMemoryIntegrationEventBus.Instance.Subscribe(() => _context.Resolve<TIntegrationEventHandler>());
            return this;
        }
    }
}
