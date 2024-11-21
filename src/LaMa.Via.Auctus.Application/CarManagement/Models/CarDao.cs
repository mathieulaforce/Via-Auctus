using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.Models;

public class CarDao
{
    public Guid Id { get; init; }
    public required CarBrandDao Brand { get; init; }
    public Guid BrandId { get; init; }
    public required CarModelDao Model { get; init; }
    public Guid ModelId { get; init; }

    public required CarModelVersionDao Version { get; init; }
    public Guid VersionId { get; init; }

    public required EngineDao Engine { get; init; }
    public Guid EngineId { get; init; }
    public CarRegistration? Registration { get; init; }
}