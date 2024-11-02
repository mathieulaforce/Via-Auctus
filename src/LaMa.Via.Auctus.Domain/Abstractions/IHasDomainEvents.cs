namespace LaMa.Via.Auctus.Domain.Abstractions;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> GetDomainEvents();
    public void ClearDomainEvents();
}