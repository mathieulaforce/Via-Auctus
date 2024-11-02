using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
using LaMa.Via.Auctus.Domain.CarManagement.Events;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarId : AggregateRootId<Guid>
{
    private CarId(Guid value)
    {
        Value = value;
    }

    public override Guid Value { get; protected set; }
 
    public static CarId CreateUnique()
    {
        return new CarId(UniqueIdGenerator.Generate());
    }

    public static CarId Create(Guid value)
    {
        return new CarId(value);
    }
}

public class Car : AggregateRoot<CarId, Guid>
{
    private Car(CarId carId, CarBrand brand, CarModel model, CarModelVersion version, Engine engine,
        CarRegistration? registration) : base(carId)
    {
        Brand = brand;
        Model = model;
        Version = version;
        Engine = engine;
        Registration = registration;
    }

    public CarBrand Brand { get; private set; }
    public CarModel Model { get; private set; }
    public CarModelVersion Version { get; private set; }
    public Engine Engine { get; private set; }
    public CarRegistration? Registration { get; private set; }

    public ErrorOr<Success> Register(string licensePlate, DateOnly firstRegistration, DateOnly registrationExpiry)
    {
        if (Registration != null)
        {
            return CarErrors.CarAlreadyRegistered(Id);
        }

        var registrationResult = CarRegistration.Create(licensePlate, firstRegistration, registrationExpiry);
        if (registrationResult.IsError)
        {
            return registrationResult.Errors;
        }

        Registration = registrationResult.Value;
        RaiseDomainEvent(new CarRegisteredDomainEvent(Id));
        return Result.Success;
    }

    public static ErrorOr<Car> Create(CarBrand brand, CarModel model, CarModelVersion version, Engine engine,
        CarRegistration? registration)
    {
        var errors = ValidateCreateCar(brand, model, version, engine);
        if (errors.HasErrors)
        {
            return errors;
        }

        var carId = CarId.CreateUnique();
        var car = new Car(carId, brand, model, version, engine, registration);
        car.RaiseDomainEvent(new CarCreatedDomainEvent(carId));
        return car;
    }

    private static ErrorCollection ValidateCreateCar(CarBrand brand, CarModel model, CarModelVersion version,
        Engine engine)
    {
        var errors = new ErrorCollection();
        if (model.CarBrandId != brand.Id)
        {
            errors += CarErrors.CarBrandDoesNotSupportModel(brand.Id, model.Id);
        }

        if (version.CarModelId != model.Id)
        {
            errors += CarErrors.CarModelDoesNotSupportVersion(brand.Id, model.Id, version.Id);
        }

        if (!version.HasEngine(engine))
        {
            errors += CarErrors.CarVersionDoesNotSupportEngine(brand.Id, model.Id, version.Id, engine.Id);
        }

        return errors;
    }
}