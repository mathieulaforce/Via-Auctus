namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record EngineEfficiency
{
    public decimal Value { get; private set; }
    public string Unit { get; private set; }

    private EngineEfficiency(decimal value, string unit)
    {
        Value = value;
        Unit = unit;
    }

    public bool IsUnknown => Value == 0 && string.IsNullOrEmpty(Unit);

    public static EngineEfficiency Unknown => new(0, string.Empty);

    public static EngineEfficiency LPer100Km(decimal value)
    {
        return new EngineEfficiency(value, "L/100km");
    }

    public static EngineEfficiency WhPerKm(decimal value)
    {
        return new EngineEfficiency(value, "Wh/km");
    }
}