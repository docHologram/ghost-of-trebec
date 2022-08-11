using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostOfTrebec.Core.InnerCore
{
    public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>>
    {
        [Obsolete("Should only be used ORM")]
        protected Entity() { }

        protected Entity(TId identifier)
        {
            Identifier = identifier;
        }

        public TId Identifier { get; } = default!;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Entity<TId> other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (Identifier is null)
            {
                return false;
            }

            return Identifier.Equals(other.Identifier);
        }

        public override int GetHashCode()
        {
            return Identifier?.GetHashCode() ?? 0;
        }

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);
    }
}
