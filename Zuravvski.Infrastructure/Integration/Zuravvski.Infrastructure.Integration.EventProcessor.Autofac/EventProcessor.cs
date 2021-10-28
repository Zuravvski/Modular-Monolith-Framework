﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Zuravvski.DDD;

namespace Zuravvski.Infrastructure.Integration.EventProcessor
{
    internal sealed class EventProcessor : IEventProcessor
    {
        private readonly IEventMapper _eventMapper;
        private readonly IIntegrationEventBusClient _busClient;
        private readonly IComponentContext _context;

        public EventProcessor(IEventMapper eventMapper, IIntegrationEventBusClient busClient, IComponentContext context)
        {
            _eventMapper = eventMapper;
            _busClient = busClient;
            _context = context;
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
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlersType = typeof(IEnumerable<>).MakeGenericType(handlerType);
                var resolved = _context.TryResolve(handlersType, out dynamic domainEventHandlers);

                if (resolved)
                {
                    foreach (var domainEventHandler in domainEventHandlers)
                    {
                        await domainEventHandler.Handle(domainEvent);
                    }
                }

                var integrationEvent = _eventMapper.Map(domainEvent);

                if (integrationEvent is { })
                {
                    integrationEvents.Add(integrationEvent);
                }
            }
            return integrationEvents;
        }
    }
}
