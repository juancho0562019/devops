namespace Bext.Reps.Domain.Primitives;

public abstract class BaseEntity<TKey> : IEquatable<BaseEntity<TKey>> where TKey : notnull
{
    public virtual TKey Id { get; init; } = default(TKey)!;

    protected BaseEntity()
    {
    }

    protected BaseEntity(TKey id) => Id = id;

    public static bool operator ==(BaseEntity<TKey> entity1, BaseEntity<TKey> entity2) =>
        ReferenceEquals(entity1, entity2) || (((object)entity1 != null) && entity1.Equals(entity2));

    public static bool operator !=(BaseEntity<TKey> entity1, BaseEntity<TKey> entity2) => !(entity1 == entity2);

    public bool Equals(BaseEntity<TKey>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        if (Id == null)
        {
            return false;
        }

        if (obj == null)
        {
            return false;
        }

        if (!(obj is BaseEntity<TKey> other))
        {
            return false;
        }

        if (other.GetType() != this.GetType())
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (IsTransient() || other.IsTransient())
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    private bool IsTransient()
    {
        return Id is null || Id.Equals(default(TKey));
    }

    public override int GetHashCode() => this.Id?.GetHashCode() ?? 0;

    public override string ToString() => this.Id?.ToString() ?? string.Empty;
}