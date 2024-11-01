namespace LaMa.Via.Auctus.Domain.Abstractions;

public abstract class AggregateRootId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}