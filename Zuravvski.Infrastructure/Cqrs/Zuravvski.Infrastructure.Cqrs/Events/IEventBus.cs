using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Events.Abstractions
{
    public interface IEventBus
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
