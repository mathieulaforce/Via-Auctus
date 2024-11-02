using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement.Events;

public sealed record CarBrandCreatedDomainEvent(CarBrandId CarId) : IDomainEvent;