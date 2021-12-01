using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Events.Abstractions
{
    public interface IEventHandler<TEvent> where TEvent : class, IEvent
    {
        public Task Handle(TEvent @event);
    }
}
