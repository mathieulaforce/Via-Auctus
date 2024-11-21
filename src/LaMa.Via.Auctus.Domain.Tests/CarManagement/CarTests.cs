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
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines,
            SupportedImage.Create("test.png").Value);

        var teslaModelYResult = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, engine.Id)
            .Build();
        teslaModelYResult.IsError.Should().BeFalse();
        var teslaModelY = teslaModelYResult.Value;
        var carCreatedEvent = teslaModelY.ContainsOneDomainEventOfType<CarCreatedDomainEvent>();

        teslaModelY.Id.Value.Should().NotBeEmpty();
        //Brand
        teslaModelY.Id.Value.Should().NotBeEmpty();
        teslaModelY.Id.Should().Be(carCreatedEvent.CarId);
        //Brand
        teslaModelY.BrandId.Value.Should().NotBeEmpty();
        teslaModelY.BrandId.Should().Be(tesla.Id);
        //Model
        teslaModelY.ModelId.Value.Should().NotBeEmpty();
        teslaModelY.ModelId.Should().Be(modelY.Id);
        //Engine
        teslaModelY.EngineId.Value.Should().NotBeEmpty();
        teslaModelY.EngineId.Should().Be(engine.Id);
        //Version
        teslaModelY.VersionId.Value.Should().NotBeEmpty();
        teslaModelY.VersionId.Should().Be(version.Id);
        //Registration
        teslaModelY.Registration.Should().BeNull();
    }

    [Fact]
    public void GivenCarWithRegistrationWhenCreateCarShouldCreateCarWithRegistration()
    {
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines);
        var registration = CarRegistration.Create("1-abc-234", new DateOnly(2024, 1, 11), new DateOnly(2028, 1, 11));

        var teslaModelYResult = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, engine.Id)
            .WithRegistration(registration.Value)
            .Build();

        teslaModelYResult.IsError.Should().BeFalse();
        var teslaModelY = teslaModelYResult.Value;
        var carCreatedEvent = teslaModelY.ContainsOneDomainEventOfType<CarCreatedDomainEvent>();
        teslaModelY.Id.Value.Should().NotBeEmpty();
        teslaModelY.Id.Should().Be(carCreatedEvent.CarId);
        //Brand
        teslaModelY.BrandId.Value.Should().NotBeEmpty();
        teslaModelY.BrandId.Should().Be(tesla.Id);
        //Model
        teslaModelY.ModelId.Value.Should().NotBeEmpty();
        teslaModelY.ModelId.Should().Be(modelY.Id);
        //Engine
        teslaModelY.EngineId.Value.Should().NotBeEmpty();
        teslaModelY.EngineId.Should().Be(engine.Id);
        //Version
        teslaModelY.VersionId.Value.Should().NotBeEmpty();
        teslaModelY.VersionId.Should().Be(version.Id);
        //Registration
        teslaModelY.Registration.LicensePlate.Should().Be("1-abc-234");
        teslaModelY.Registration.FirstRegistrationDate.Should().Be(new DateOnly(2024, 1, 11));
        teslaModelY.Registration.RegistrationExpiryDate.Should().Be(new DateOnly(2028, 1, 11));
    }

    [Fact]
    public void GivenUnregisteredCarWhenRegisteringThenSaveCarRegistration()
    {
        var car = CarObjectMother.UnregisteredTeslaModelYAllWheelDrive();
        var registration = CarRegistration.Create("1-abc-234", new DateOnly(2024, 1, 11), new DateOnly(2028, 1, 11))
            .Value;

        car.Register(registration.LicensePlate, registration.FirstRegistrationDate,
            registration.RegistrationExpiryDate);

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
        var result = car.Register(registration.LicensePlate, registration.FirstRegistrationDate,
            registration.RegistrationExpiryDate);

        car.Registration.Should().NotBe(registration);
        var domainEvent = car.GetDomainEvents().Should().BeEmpty();
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors.CarAlreadyRegistered(car.Id).Code);
    }

    [Fact]
    public void GivenInvalidRegistrationDatesCarWhenRegisteringThenReturnsError()
    {
        var car = CarObjectMother.UnregisteredTeslaModelYAllWheelDrive();

        car.ClearDomainEvents();
        var result = car.Register("new", new DateOnly(2050, 1, 11), new DateOnly(2028, 1, 11));

        car.Registration.Should().BeNull();
        var domainEvent = car.GetDomainEvents().Should().BeEmpty();
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarRegistrationErrors
            .FirstRegistrationDateAfterExpiryDate(new DateOnly(2050, 1, 11), new DateOnly(2028, 1, 11)).Code);
    }

    [Fact]
    public void CreateCarWithIncorrectBrandModelThenReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.BmwX1();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines);

        var result = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, engine.Id)
            .Build();
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors.CarBrandDoesNotSupportModel(tesla.Id, modelY.Id).Code);
    }

    [Fact]
    public void CreateCarWithIncorrectVersionReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(CarModelObjectMother.BmwX1().Id, "Long Range All-Wheel Drive", 2024,
            engines);

        var result = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, engine.Id)
            .Build();

        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should()
            .Be(CarErrors.CarModelDoesNotSupportVersion(tesla.Id, modelY.Id, version.Id).Code);
    }

    [Fact]
    public void CreateCarWithUnsupportedEngineReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla();
        var modelY = CarModelObjectMother.TeslaModelY();
        var engine = EngineObjectMother.UnknownElectricEngine;
        var unsupportedEngine = Engine.Create("Unsupported", FuelType.Create("unknown").Value, null, null,
            EngineEfficiency.Unknown);
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(modelY.Id, "Long Range All-Wheel Drive", 2024, engines);

        var result = Car.Define()
            .WithBrand(tesla)
            .WithModel(modelY)
            .WithVersion(version, unsupportedEngine.Id)
            .Build();

        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarErrors
            .CarVersionDoesNotSupportEngine(tesla.Id, modelY.Id, version.Id, unsupportedEngine.Id).Code);
    }
}