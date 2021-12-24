using Zuravvski.DDD;

namespace Zuravvski.Infrastructure.Integration.EventProcessor
{
    public interface IEventMapper
    {
        public IntegrationEvent Map(DomainEvent @event);
    }
}
