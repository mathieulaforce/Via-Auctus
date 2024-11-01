using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed class CarId : AggregateRootId<Guid>
{
    private CarId(Guid value)
    {
        Value = value;
    }

    public override Guid Value { get; protected set; }

    protected override IEnumerable<object?> GetEqualityValues()
    {
        yield return Value;
    }

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

    public void Register(string licensePlate, DateOnly firstRegistration, DateOnly registrationExpiry)
    {
        Registration = CarRegistration.Create(licensePlate, firstRegistration, registrationExpiry);
    }

    public static Car Create(CarBrand brand, CarModel model, CarModelVersion version, Engine engine,
        CarRegistration? registration)
    {
        if (model.CarBrandId != brand.Id)
        {
            throw new ArgumentException($"Brand id '{brand.Id.Value}' does not support model id'{model.Id.Value}'");
        }

        if (version.CarModelId != model.Id)
        {
            throw new ArgumentException($"model id '{model.Id.Value}' does not support version id'{version.Id.Value}'");
        }

        if (!version.HasEngine(engine))
        {
            throw new ArgumentException($"Version '{version.Id.Value}' does not support engine: '{engine.Id.Value}'");
        }

        var carId = CarId.CreateUnique();
        return new Car(carId, brand, model, version, engine, registration);
    }
}