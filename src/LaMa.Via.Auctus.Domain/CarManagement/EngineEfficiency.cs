namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record EngineEfficiency
{
    private EngineEfficiency(decimal value, string unit)
    {
        Value = value;
        Unit = unit;
    }

    public decimal Value { get; }
    public string Unit { get; }

    public bool IsUnknown => Value == 0 && string.IsNullOrEmpty(Unit);

    public static EngineEfficiency Unknown => new(0, string.Empty);

    public static EngineEfficiency Create(decimal value, string unit)
    {
        return new EngineEfficiency(value, unit);
    }

    public static EngineEfficiency LPer100Km(decimal value)
    {
        return new EngineEfficiency(value, "L/100km");
    }

    public static EngineEfficiency WhPerKm(decimal value)
    {
        return new EngineEfficiency(value, "Wh/km");
    }
}