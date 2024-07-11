namespace Bext.Reps.Domain.Primitives
{
    public abstract class AggregateRoot<TKey> : BaseEntity<TKey> where TKey : notnull
    {
        protected AggregateRoot(TKey id) : base(id)
        {
        }
    }
}