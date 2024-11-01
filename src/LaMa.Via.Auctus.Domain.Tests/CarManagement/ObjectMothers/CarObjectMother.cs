using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public class CarObjectMother
{
    public static Car UnregisteredTeslaModelYAllWheelDrive()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);
        var teslaModelY = Car.Create(tesla, modelY, version, engine, null);
        return teslaModelY;
    }

    public static Car RegisteredTeslaModelYAllWheelDrive()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);
        var registration = CarRegistration.Create("1-ABC-234", new DateOnly(2024, 11, 1), new DateOnly(2028, 11, 1));
        var teslaModelY = Car.Create(tesla, modelY, version, engine, registration);
        return teslaModelY;
    }
}