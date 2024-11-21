using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Builders;

public interface ICarBuilderBrandStep
{
    ICarBuilderModelStep WithBrand(CarBrand brand);
}

public interface ICarBuilderModelStep
{
    ICarBuilderVersionStep WithModel(CarModel model);
}

public interface ICarBuilderVersionStep
{
    ICarBuilderOptionalSteps WithVersion(CarModelVersion version, EngineId engine);
}

public interface ICarBuilderOptionalSteps
{
    ICarBuilderOptionalSteps WithRegistration(CarRegistration registration);
    ErrorOr<Car> Build();
}

public interface ICarBuilder : ICarBuilderBrandStep, ICarBuilderModelStep, ICarBuilderVersionStep,
    ICarBuilderOptionalSteps
{
}