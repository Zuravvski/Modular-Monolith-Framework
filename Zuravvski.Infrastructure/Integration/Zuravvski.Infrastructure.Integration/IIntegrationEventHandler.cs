using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Integration
{
    public interface IIntegrationEventHandler<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        public Task Handle(TIntegrationEvent @event);
    }
}
