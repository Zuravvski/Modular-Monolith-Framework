using Microsoft.Extensions.DependencyInjection;
using Zuravvski.DDD;
using Zuravvski.Infrastructure.Integration.EventProcessor;

namespace Zuravvski.Infrastructure.Integration.Memory.DependencyInjection
{
    internal sealed class SynchronousEventProcessor : IEventProcessor
    {
        private readonly IEventMapper _eventMapper;
        private readonly IIntegrationEventBusClient _busClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SynchronousEventProcessor(IEventMapper eventMapper,
                                         IIntegrationEventBusClient busClient,
                                         IServiceScopeFactory serviceScopeFactory)
        {
            _eventMapper = eventMapper;
            _busClient = busClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Process(IEnumerable<DomainEvent> events)
        {
            if (events is null)
            {
                return;
            }

            var integrationEvents = await HandleDomainEvents(events);

            if (integrationEvents.Any())
            {
                await _busClient.Publish(integrationEvents);
            }
        }

        private async Task<IEnumerable<IntegrationEvent>> HandleDomainEvents(IEnumerable<DomainEvent> domainEvents)
        {
            var integrationEvents = new List<IntegrationEvent>();
            using var scope = _serviceScopeFactory.CreateScope();

            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                dynamic handlers = scope.ServiceProvider.GetServices(handlerType);
                foreach (var handler in handlers)
                {
                    await handler.HandleAsync((dynamic) domainEvent);
                }

                var integrationEvent = _eventMapper.Map(domainEvent);

                if (integrationEvent is not null)
                {
                    integrationEvents.Add(integrationEvent);
                }
            }
            return integrationEvents;
        }
    }
}
