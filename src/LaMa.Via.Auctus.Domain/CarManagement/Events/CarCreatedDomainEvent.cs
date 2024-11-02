using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement.Events;

public sealed record CarCreatedDomainEvent(CarId CarId) : IDomainEvent;