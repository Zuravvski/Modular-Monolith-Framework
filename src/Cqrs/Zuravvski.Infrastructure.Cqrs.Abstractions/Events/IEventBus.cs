using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Abstractions.Events
{
    public interface IEventBus
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
