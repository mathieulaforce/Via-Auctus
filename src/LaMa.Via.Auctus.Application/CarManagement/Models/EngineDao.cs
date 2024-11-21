using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.Models;

public class EngineDao
{
    public Guid Id { get; init; }

    public required string Name { get; init; }
    public required string FuelType { get; init; }
    public int? HorsePower { get; init; }
    public int? Torque { get; init; }
    public required EngineEfficiency Efficiency { get; init; }
}