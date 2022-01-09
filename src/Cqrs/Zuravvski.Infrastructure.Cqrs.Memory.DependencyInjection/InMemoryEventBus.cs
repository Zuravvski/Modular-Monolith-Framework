using Microsoft.Extensions.DependencyInjection;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Events;

namespace Zuravvski.Infrastructure.Cqrs.Memory.DependencyInjection
{
    internal sealed class InMemoryEventBus : IEventBus
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InMemoryEventBus(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(TEvent), "Command cannot be null");
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>()
                ?.Select(handler => handler.Handle(@event));
            
            if (handlers is not null)
            {
                await Task.WhenAll(handlers);
            }
        }
    }
}
