using System.Collections;

namespace LaMa.Via.Auctus.Domain.Abstractions;

public abstract class EntityCollection<T, TId> : IEnumerable<T> where T : Entity<TId> where TId : notnull
{
    protected readonly List<T> Items = new();

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}