using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Abstractions.Events
{
    public interface IEventHandler<TEvent> where TEvent : class, IEvent
    {
        public Task Handle(TEvent @event);
    }
}
