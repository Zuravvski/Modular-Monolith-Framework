using System;
using Zuravvski.DDD.Exceptions;

namespace Zuravvski.DDD
{
    public abstract class EntityId : IEquatable<EntityId>
    {
        public Guid Value { get; protected init; }

        protected EntityId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidEntityIdException();
            }

            Value = value;
        }

        public bool Equals(EntityId other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EntityId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator Guid(EntityId id)
            => id.Value;

        public static bool operator ==(EntityId obj1, EntityId obj2)
            => obj1.Equals(obj2);

        public static bool operator !=(EntityId obj1, EntityId obj2)
            => !(obj1 == obj2);

        public override string ToString() => Value.ToString();
    }
}
