using System;

namespace Zuravvski.DDD
{
    public abstract class DomainEvent
    {
        public Guid Id { get; }

        protected DomainEvent() : this(Guid.NewGuid())
        {
        }

        protected DomainEvent(Guid id)
        {
            Id = id;
        }
    }
}
