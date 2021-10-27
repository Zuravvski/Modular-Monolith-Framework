using System.Collections.Generic;

namespace Zuravvski.DDD
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId> where TEntityId : EntityId
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public int Version { get; protected set; }
        public IEnumerable<DomainEvent> DomainEvents => _domainEvents;

        protected AggregateRoot(TEntityId id) : base(id)
        {
        }

        protected void RaiseDomainEvent(DomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}
