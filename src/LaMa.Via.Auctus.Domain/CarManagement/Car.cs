using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Builders;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
using LaMa.Via.Auctus.Domain.CarManagement.Events;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarId
{
    private CarId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static CarId CreateUnique()
    {
        return new CarId(UniqueIdGenerator.Generate());
    }

    public static CarId Create(Guid value)
    {
        return new CarId(value);
    }
}

public class Car : AggregateRoot<CarId>
{
    private Car()
    {
    }

    private Car(CarId carId, CarBrandId brand, CarModelId model, CarModelVersionId version, EngineId engineId,
        CarRegistration? registration) : base(carId)
    {
        BrandId = brand;
        ModelId = model;
        VersionId = version;
        EngineId = engineId;
        Registration = registration;
    }

    public CarBrandId BrandId { get; private set; }
    public CarModelId ModelId { get; private set; }
    public CarModelVersionId VersionId { get; private set; }
    public EngineId EngineId { get; set; }
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

    private static Car Create(CarId carId, CarBrandId brandId, CarModelId modelId, CarModelVersionId versionId,
        EngineId engineId, CarRegistration? registration)
    {
        var car = new Car(carId, brandId, modelId, versionId, engineId, registration);
        car.RaiseDomainEvent(new CarCreatedDomainEvent(carId));
        return car;
    }

    public static ICarBuilderBrandStep Define()
    {
        return CarBuilder.Create();
    }

    public sealed class CarBuilder : ICarBuilder
    {
        private CarBrand? _brand;
        private EngineId? _engineId;
        private CarModel? _model;
        private CarRegistration? _registration;
        private CarModelVersion? _version;

        public ICarBuilderModelStep WithBrand(CarBrand brand)
        {
            _brand = brand;
            return this;
        }

        public ICarBuilderVersionStep WithModel(CarModel model)
        {
            _model = model;
            return this;
        }

        public ICarBuilderOptionalSteps WithVersion(CarModelVersion version, EngineId engine)
        {
            _version = version;
            _engineId = engine;
            return this;
        }

        public ICarBuilderOptionalSteps WithRegistration(CarRegistration registration)
        {
            _registration = registration;
            return this;
        }

        public ErrorOr<Car> Build()
        {
            var result = ValidateCreateCar(_brand, _model, _version, _engineId);
            if (result.IsError)
            {
                return result.Errors;
            }

            var carId = CarId.CreateUnique();
            var (brandId, modelId, versionId, engineId) = result.Value;
            var car = Car.Create(carId, brandId, modelId, versionId, engineId, _registration);
            return car;
        }

        public static ICarBuilderBrandStep Create()
        {
            return new CarBuilder();
        }

        private static ErrorOr<(CarBrandId, CarModelId, CarModelVersionId, EngineId)> ValidateCreateCar(CarBrand? brand,
            CarModel? model, CarModelVersion? version,
            EngineId? engineId)
        {
            ArgumentNullException.ThrowIfNull(brand, nameof(brand));
            ArgumentNullException.ThrowIfNull(model, nameof(model));
            ArgumentNullException.ThrowIfNull(version, nameof(version));
            ArgumentNullException.ThrowIfNull(engineId, nameof(engineId));

            var errors = new ErrorCollection();
            if (!brand.SupportsModel(model))
            {
                errors += CarErrors.CarBrandDoesNotSupportModel(brand.Id, model.Id);
            }

            if (!model.SupportsVersion(version))
            {
                errors += CarErrors.CarModelDoesNotSupportVersion(brand.Id, model.Id, version.Id);
            }

            if (!version.HasEngine(engineId))
            {
                errors += CarErrors.CarVersionDoesNotSupportEngine(brand.Id, model.Id, version.Id, engineId);
            }

            if (errors.HasErrors)
            {
                return errors.ToList();
            }

            return (brand.Id, model.Id, version.Id, engineId);
        }
    }
}