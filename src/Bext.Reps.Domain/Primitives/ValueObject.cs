namespace Bext.Reps.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return Equals(a, b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !Equals(a, b);
        }
    }
}
