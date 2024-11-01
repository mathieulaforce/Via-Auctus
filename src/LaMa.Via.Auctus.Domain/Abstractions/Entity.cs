namespace LaMa.Via.Auctus.Domain.Abstractions;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; } = id;

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

// private readonly List<IDomainEvent> _domainEvents = new();
// public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
// public void AddDomainEvent(IDomainEvent domainEvent)
// {
//     _domainEvents.Add(domainEvent);
// }
//
// public void ClearDomainEvents()
// {
//     _domainEvents.Clear();
// }