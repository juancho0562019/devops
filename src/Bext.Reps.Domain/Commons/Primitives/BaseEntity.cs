namespace Bext.Reps.Domain.Commons.Primitives;

public abstract class BaseEntity<TKey> : BaseAuditableEntity where TKey : notnull
{
    public virtual TKey Id { get; init; } = default!;

    protected BaseEntity()
    {
    }

    protected BaseEntity(TKey id) => Id = id;

}
