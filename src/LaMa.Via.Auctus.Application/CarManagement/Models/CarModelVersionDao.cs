namespace LaMa.Via.Auctus.Application.CarManagement.Models;

public class CarModelVersionDao
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public int Year { get; init; }

    public Guid CarModelId { get; init; }
    public required CarModelDao CarModel { get; init; }

    public ICollection<EngineDao> Engines { get; init; } = new List<EngineDao>();
}