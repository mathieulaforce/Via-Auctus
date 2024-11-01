using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public class EngineObjectMother
{
    public static readonly Engine UnknownElectricEngine =
        Engine.Create("Unknown", FuelType.Create("Electric"), null, null, null);
}