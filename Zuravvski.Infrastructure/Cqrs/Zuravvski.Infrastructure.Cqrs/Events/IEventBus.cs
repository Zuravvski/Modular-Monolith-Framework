using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Events
{
    public interface IEventBus
    {
        public Task Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}
