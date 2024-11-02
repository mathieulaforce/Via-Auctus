using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.Tests.Helpers;

public static class DomainEventExtensions
{
    public static TDomainEvent ContainsOneDomainEventOfType<TDomainEvent>(this IHasDomainEvents hasDomainEvents)
    {
        var events = ContainsOneOrMultipleDomainEventOfType<TDomainEvent>(hasDomainEvents);
        return events.Should().ContainSingle().Subject;
    }
    
    public static List<TDomainEvent> ContainsOneOrMultipleDomainEventOfType<TDomainEvent>(this IHasDomainEvents hasDomainEvents)
    {
        var events = hasDomainEvents.GetDomainEvents().Where(e => e is TDomainEvent).Cast<TDomainEvent>().ToList();
        events.Should().NotBeEmpty("There should be one or more domain events {DomainEvent}", typeof(TDomainEvent));
        return events;
    }
}