using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public class Engines : EntityCollection<Engine, EngineId>
{
    public static Engines Empty => new();

    public bool CanAddEngine(string name, FuelType fuelType, int? horsePower, int? torque, decimal? efficiency)
    {
        var engine = Engine.Create(name, fuelType, horsePower, torque, efficiency);
        return CanAddEngine(engine);
    }

    public Engines AddEngine(string name, FuelType fuelType, int? horsePower, int? torque, decimal? efficiency)
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

    public Engines AddEngine(Engine engine)
    {
        if (!CanAddEngine(engine))
        {
            // TODO Exception handling
            throw new Exception("Can't add vehicle model version");
        }

        Items.Add(engine);
        return this;
    }
}