using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Events;
using LaMa.Via.Auctus.Domain.Shared;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;
using LaMa.Via.Auctus.Domain.Tests.Helpers;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class CarTests
{
    [Fact]
    public void GivenNoRegistrationWhenCreateCarShouldCreateCarWithoutRegistration()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, SupportedImage.Create("test.png"));

        var teslaModelY = Car.Create(tesla, modelY, version, engine, null);

        teslaModelY.Id.Value.Should().NotBeEmpty();
        //Brand
        teslaModelY.Brand.Id.Value.Should().NotBeEmpty();
        teslaModelY.Brand.Name.Should().Be("Tesla");
        teslaModelY.Brand.Theme.Logo.Should().NotBeNull();
        teslaModelY.Brand.Theme.FontFamily.Should().NotBeNull();
        teslaModelY.Brand.Theme.PrimaryColor.Should().NotBeNull();
        teslaModelY.Brand.Theme.SecondaryColor.Should().NotBeNull();
        //Model
        teslaModelY.Model.Id.Value.Should().NotBeEmpty();
        teslaModelY.Model.Name.Should().Be("Tesla Y");
        teslaModelY.Model.CarBrandId.Should().Be(teslaModelY.Brand.Id);
        //Engine
        teslaModelY.Engine.Id.Value.Should().NotBeEmpty();
        teslaModelY.Engine.Name.Should().Be("Unknown");
        teslaModelY.Engine.FuelType.Should().Be(EngineObjectMother.UnknownElectricEngine.FuelType);
        //Version
        teslaModelY.Version.Id.Value.Should().NotBeEmpty();
        teslaModelY.Version.Name.Should().Be("Long Range All-Wheel Drive");
        teslaModelY.Version.Year.Should().Be(2024);
        teslaModelY.Version.Image.Url.Should().Be("test.png");
        teslaModelY.Version.HasEngine(engine).Should().BeTrue();
        //Registration
        teslaModelY.Registration.Should().BeNull();
    }

    [Fact]
    public void GivenCarWithRegistrationWhenCreateCarShouldCreateCarWithRegistration()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);
        var registration = CarRegistration.Create("1-abc-234", new DateOnly(2024, 1, 11), new DateOnly(2028, 1, 11));

        var teslaModelY = Car.Create(tesla, modelY, version, engine, registration);

        var carCreatedEvent = teslaModelY.ContainsOneDomainEventOfType<CarCreatedDomainEvent>();
        teslaModelY.Id.Value.Should().NotBeEmpty();
        teslaModelY.Id.Should().Be(carCreatedEvent.CarId);
        //Brand
        teslaModelY.Brand.Id.Value.Should().NotBeEmpty();
        teslaModelY.Brand.Name.Should().Be("Tesla");
        //Model
        teslaModelY.Model.Id.Value.Should().NotBeEmpty();
        teslaModelY.Model.Name.Should().Be("Tesla Y");
        teslaModelY.Model.Image.Should().BeNull();
        teslaModelY.Model.CarBrandId.Should().Be(teslaModelY.Brand.Id);
        //Engine
        teslaModelY.Engine.Id.Value.Should().NotBeEmpty();
        teslaModelY.Engine.Name.Should().Be("Unknown");
        teslaModelY.Engine.FuelType.Should().Be(EngineObjectMother.UnknownElectricEngine.FuelType);
        teslaModelY.Engine.Efficiency.IsUnknown.Should().BeTrue();
        teslaModelY.Engine.HorsePower.Should().BeNull();
        teslaModelY.Engine.Torque.Should().BeNull();
        //Version
        teslaModelY.Version.Id.Value.Should().NotBeEmpty();
        teslaModelY.Version.Name.Should().Be("Long Range All-Wheel Drive");
        teslaModelY.Version.Year.Should().Be(2024);
        teslaModelY.Version.HasEngine(engine).Should().BeTrue();
        //Registration
        teslaModelY.Registration.LicensePlate.Should().Be("1-abc-234");
        teslaModelY.Registration.FirstRegistrationDate.Should().Be(new DateOnly(2024, 1, 11));
        teslaModelY.Registration.RegistrationExpiryDate.Should().Be(new DateOnly(2028, 1, 11));
    }
    
    [Fact]
    public void GivenUnregisteredCarWhenRegisteringThenSaveCarRegistration()
    {
        var car = CarObjectMother.UnregisteredTeslaModelYAllWheelDrive();
        var registration = CarRegistration.Create("1-abc-234", new DateOnly(2024, 1, 11), new DateOnly(2028, 1, 11));
        
        car.Register(registration.LicensePlate, registration.FirstRegistrationDate,registration.RegistrationExpiryDate);

        car.Registration.Should().Be(registration);
        var domainEvent = car.ContainsOneDomainEventOfType<CarRegisteredDomainEvent>();
        domainEvent.CarId.Should().Be(car.Id);
    }

    [Fact]
    public void CreateCarWithIncorrectBrandModelShouldThrowException()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.BmwX1();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);

        var createAction = () => Car.Create(tesla, modelY, version, engine, null);
        createAction.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void CreateCarWithIncorrectVersionShouldThrowException()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(CarModelObjectMother.BmwX1().Id, "Long Range All-Wheel Drive", 2024, engines, null);

        var createAction = () => Car.Create(tesla, modelY, version, engine, null);
        createAction.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateCarWithUnsupportedEngineThrowException()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var unsupportedEngine = Engine.Create("Unsupported", FuelType.Create("unknown"), null, null, null);
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);

        var createAction = () => Car.Create(tesla, modelY, version, unsupportedEngine, null);
        createAction.Should().Throw<ArgumentException>();
    }
}