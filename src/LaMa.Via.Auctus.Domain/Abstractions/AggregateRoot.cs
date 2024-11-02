namespace LaMa.Via.Auctus.Domain.Abstractions;

public abstract class AggregateRoot<TId, TIdType>(TId id) : Entity<TId>(id)
    where TId : AggregateRootId<TIdType>;