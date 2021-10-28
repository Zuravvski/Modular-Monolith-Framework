using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Events
{
    public interface IEventHandler<TEvent> where TEvent : class, IEvent
    {
        public Task Handle(TEvent @event);
    }
}
