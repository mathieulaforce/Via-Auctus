namespace LaMa.Via.Auctus.Domain.Abstractions;

public abstract record AggregateRootId<TId> 
{
    public abstract TId Value { get; protected set; }
}