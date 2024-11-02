using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public class EngineObjectMother
{
    public static readonly Engine UnknownElectricEngine =
        Engine.Create("Unknown", FuelType.Create("Electric").Value, null, null, EngineEfficiency.Unknown);
    
    public static readonly Engine TeslaModel3Motor =
        Engine.Create("Tesla Model 3 Motor", FuelType.Create("Electric").Value, 283, 30, EngineEfficiency.WhPerKm(177));
    
    public static readonly Engine Tsi15 = Engine.Create("1.5 TSI EVO", FuelType.Create("Gasoline").Value, 150, 250, EngineEfficiency.LPer100Km(6.9m));
}