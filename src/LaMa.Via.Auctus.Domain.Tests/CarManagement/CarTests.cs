using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
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
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, SupportedImage.Create("test.png").Value);

        var teslaModelYResult = Car.Create(tesla, modelY, version, engine, null);

        teslaModelYResult.IsError.Should().BeFalse();
        var teslaModelY = teslaModelYResult.Value;
        
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

        var teslaModelYResult = Car.Create(tesla, modelY, version, engine, registration.Value);

        teslaModelYResult.IsError.Should().BeFalse();
        var teslaModelY = teslaModelYResult.Value;
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
        var registration = CarRegistration.Create("1-abc-234", new DateOnly(2024, 1, 11), new DateOnly(2028, 1, 11)).Value;
        
        car.Register(registration.LicensePlate, registration.FirstRegistrationDate,registration.RegistrationExpiryDate);

        car.Registration.Should().Be(registration);
        var domainEvent = car.ContainsOneDomainEventOfType<CarRegisteredDomainEvent>();
        domainEvent.CarId.Should().Be(car.Id);
    }
    
    [Fact]
    public void GivenAlreadyRegisteredCarWhenRegisteringThenReturnsError()
    {
        var car = CarObjectMother.RegisteredTeslaModelYAllWheelDrive();
        var registration = CarRegistration.Create("new", new DateOnly(2024, 1, 11), new DateOnly(2028, 1, 11)).Value;
        
        car.ClearDomainEvents();
        var result = car.Register(registration.LicensePlate, registration.FirstRegistrationDate,registration.RegistrationExpiryDate);

        car.Registration.Should().NotBe(registration);
        var domainEvent = car.GetDomainEvents().Should().BeEmpty();
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors.CarAlreadyRegistered(car.Id).Code);
    }

    [Fact]
    public void CreateCarWithIncorrectBrandModelThenReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.BmwX1();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);

        var result = Car.Create(tesla, modelY, version, engine, null);
        
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors.CarBrandDoesNotSupportModel(tesla.Id, modelY.Id).Code);
    }
    
    [Fact]
    public void CreateCarWithIncorrectVersionReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(CarModelObjectMother.BmwX1().Id, "Long Range All-Wheel Drive", 2024, engines, null);

        var result = Car.Create(tesla, modelY, version, engine, null);
        
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors.CarModelDoesNotSupportVersion(tesla.Id, modelY.Id, version.Id).Code);
    }

    [Fact]
    public void CreateCarWithUnsupportedEngineReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla;
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var unsupportedEngine = Engine.Create("Unsupported", FuelType.Create("unknown").Value, null, null, EngineEfficiency.Unknown);
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines, null);

        var result = Car.Create(tesla, modelY, version, unsupportedEngine, null);
        
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors.CarVersionDoesNotSupportEngine(tesla.Id, modelY.Id, version.Id, unsupportedEngine.Id).Code);
    }
}