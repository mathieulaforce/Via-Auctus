using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars.Create;

public record CreateCarCommand(
    CarBrandId CarBrandId,
    CarModelId CarModelId,
    CarModelVersionId CarModelVersionId,
    EngineId EngineId,
    CarRegistrationInformation? CarRegistrationInformation)
    : ICommand<CarId>
{
}

public record CarRegistrationInformation(
    string LicencePlate,
    DateOnly FirstRegistrationDate,
    DateOnly RegistrationExpiryDate);