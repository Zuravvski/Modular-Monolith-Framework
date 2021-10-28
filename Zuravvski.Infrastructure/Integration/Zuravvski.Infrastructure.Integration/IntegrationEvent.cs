using System;

namespace Zuravvski.Infrastructure.Integration
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime Timestamp { get; }

        protected IntegrationEvent() : this(Guid.NewGuid(), DateTime.UtcNow)
        {
        }

        protected IntegrationEvent(Guid id, DateTime issuedAt)
        {
            Id = id;
            Timestamp = issuedAt;
        }
    }
}
