using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Events;

namespace Zuravvski.Infrastructure.Cqrs.Memory.Autofac
{
    internal sealed class InMemoryEventBus : IEventBus
    {
        private readonly IComponentContext _context;

        public InMemoryEventBus(IComponentContext context)
        {
            _context = context;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(TEvent), "Event cannot be null");
            }

            var didResolveHandler = _context.TryResolve(out IEnumerable<IEventHandler<TEvent>> handlers);

            if (didResolveHandler)
            {
                var handlerTasks = handlers.Select(handler => handler.Handle(@event));
                await Task.WhenAll(handlerTasks);
            }
        }
    }
}
