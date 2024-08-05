namespace Bext.Reps.Domain.Commons.Primitives
{
    public abstract class AggregateRoot<TKey> : BaseEntity<TKey> where TKey : notnull
    {
        protected AggregateRoot(TKey id) : base(id)
        {
        }
    }
}
