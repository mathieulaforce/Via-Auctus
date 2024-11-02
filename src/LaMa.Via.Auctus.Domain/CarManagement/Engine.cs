using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Events;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public class EngineId : AggregateRootId<Guid>
{
    private EngineId(Guid id)
    {
        Value = id;
    }

    public override Guid Value { get; protected set; }

    public static EngineId CreateUnique()
    {
        return new EngineId(Guid.NewGuid());
    }

    public static EngineId Create(Guid value)
    {
        return new EngineId(value);
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Value;
    }
}

public class Engine : Entity<EngineId>
{
    private Engine(EngineId id, string name, FuelType fuelType, int? horsePower, int? torque,
        EngineEfficiency efficiency) : base(id)
    {
        Name = name;
        FuelType = fuelType;
        HorsePower = horsePower;
        Torque = torque;
        Efficiency = efficiency;
    }

    public string Name { get; }
    public FuelType FuelType { get; }

    /// <summary>
    /// hp
    /// </summary>
    public int? HorsePower { get; }

    /// <summary>
    /// Nm
    /// </summary>
    public int? Torque { get; }

    /// <summary>
    /// L/100KM or wh/km, requires value objects
    /// </summary>
    public EngineEfficiency Efficiency { get; }

    public static Engine Create(string name, FuelType fuelType, int? horsePower, int? torque,
        EngineEfficiency efficiency)
    {
        var id = EngineId.CreateUnique();
        var engine = new Engine(id, name, fuelType, horsePower, torque, efficiency);
        engine.RaiseDomainEvent(new EngineCreatedDomainEvent(engine.Id));
        return engine;
    }
}