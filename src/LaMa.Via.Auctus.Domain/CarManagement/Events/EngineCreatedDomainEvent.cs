using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement.Events;

public sealed record EngineCreatedDomainEvent(EngineId EngineId) : IDomainEvent;