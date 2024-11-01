namespace LaMa.Via.Auctus.Domain.Abstractions;

public abstract class AggregateRoot<TId, TIdType>(TId id) : Entity<AggregateRootId<TIdType>>(id)
    where TId : AggregateRootId<TIdType>;