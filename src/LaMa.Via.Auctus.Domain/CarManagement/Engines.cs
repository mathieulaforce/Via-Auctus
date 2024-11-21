using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public class Engines : EntityCollection<Engine, EngineId>
{
    public static Engines Empty => new();

    public bool CanAddEngine(string name, FuelType fuelType, int? horsePower, int? torque, EngineEfficiency efficiency)
    {
        var engine = Engine.Create(name, fuelType, horsePower, torque, efficiency);
        return CanAddEngine(engine);
    }

    public Engines AddEngine(string name, FuelType fuelType, int? horsePower, int? torque, EngineEfficiency efficiency)
    {
        var engine = Engine.Create(name, fuelType, horsePower, torque, efficiency);
        AddEngine(engine);
        return this;
    }

    public bool Contains(Engine engine)
    {
        return Items.Contains(engine);
    }

    public bool Contains(EngineId engineId)
    {
        return Items.Any(engine => engine.Id == engineId);
    }

    public bool CanAddEngine(Engine engine)
    {
        return !Items.Any(existingEngine => engine.Equals(existingEngine)
                                            || (existingEngine.FuelType.Equals(engine.FuelType)
                                                && existingEngine.Name.Equals(engine.Name,
                                                    StringComparison.InvariantCultureIgnoreCase)));
    }

    public ErrorOr<Engines> AddEngine(Engine engine)
    {
        if (!CanAddEngine(engine))
        {
            return EnginesErrors.EngineAlreadyRegistered(engine.Id, engine.Name);
        }

        Items.Add(engine);
        return this;
    }

    public Engine GetEngineById(EngineId engineId)
    {
        return Items.Single(engine => engine.Id == engineId);
    }
}