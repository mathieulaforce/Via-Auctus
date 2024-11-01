using LaMa.Via.Auctus.Domain.Abstractions;

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
        decimal? efficiency) : base(id)
    {
        Name = name;
        FuelType = fuelType;
        HorsePower = horsePower;
        Torque = torque;
        Efficiency = efficiency;
    }

    public string Name { get; }
    public FuelType FuelType { get; }
    public int? HorsePower { get; }
    public int? Torque { get; }
    public decimal? Efficiency { get; }

    public static Engine Create(string name, FuelType fuelType, int? horsePower, int? torque, decimal? efficiency)
    {
        var id = EngineId.CreateUnique();
        return new Engine(id, name, fuelType, horsePower, torque, efficiency);
    }
}