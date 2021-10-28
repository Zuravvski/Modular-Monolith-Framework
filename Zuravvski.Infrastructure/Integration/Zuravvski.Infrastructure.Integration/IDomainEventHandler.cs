using System.Threading.Tasks;
using Zuravvski.DDD;

namespace Zuravvski.Infrastructure.Integration
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : DomainEvent
    {
        public Task Handle(TDomainEvent @event);
    }
}
