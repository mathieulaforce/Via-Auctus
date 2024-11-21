using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public class CarObjectMother
{
    public static Car UnregisteredTeslaModelYAllWheelDrive()
    {
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines);
        var teslaModelY = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, engine.Id)
            .Build();

        return teslaModelY.Value;
    }

    public static Car RegisteredTeslaModelYAllWheelDrive()
    {
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines);
        var registration = CarRegistration.Create("1-ABC-234", new DateOnly(2024, 11, 1), new DateOnly(2028, 11, 1));
        var teslaModelY = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, engine.Id)
            .WithRegistration(registration.Value)
            .Build();
        return teslaModelY.Value;
    }
}