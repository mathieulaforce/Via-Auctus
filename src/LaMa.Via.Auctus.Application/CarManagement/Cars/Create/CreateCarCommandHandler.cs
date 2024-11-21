using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarModels;
using LaMa.Via.Auctus.Application.CarManagement.CarModelVersions;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars.Create;

internal class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, CarId>
{
    private readonly ICarBrandWriteRepository _carBrandWriteRepository;
    private readonly ICarModelVersionWriteRepository _carModelVersionWriteRepository;
    private readonly ICarModelWriteRepository _carModelWriteRepository;
    private readonly ICarWriteRepository _carWriteRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateCarCommandHandler(ICarWriteRepository carWriteRepository,
        ICarBrandWriteRepository carBrandWriteRepository,
        ICarModelWriteRepository carModelWriteRepository,
        ICarModelVersionWriteRepository carModelVersionWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _carWriteRepository = carWriteRepository;
        _carBrandWriteRepository = carBrandWriteRepository;
        _carModelVersionWriteRepository = carModelVersionWriteRepository;
        _carModelWriteRepository = carModelWriteRepository;

        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CarId>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var errorOrValues = await ValidateAndGetValues(request, cancellationToken);
        if (errorOrValues.IsError)
        {
            return errorOrValues.Errors;
        }

        var (brand, model, version, registration) = errorOrValues.Value;

        var result = Car.Define()
            .WithBrand(brand)
            .WithModel(model)
            .WithVersion(version, request.EngineId)
            .WithRegistration(registration)
            .Build();

        if (result.IsError)
        {
            return result.Errors;
        }

        var car = result.Value;

        await _carWriteRepository.Add(car, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return car.Id;
    }

    private async Task<ErrorOr<(CarBrand, CarModel, CarModelVersion, CarRegistration)>> ValidateAndGetValues(
        CreateCarCommand request,
        CancellationToken cancellationToken)
    {
        var errors = new ErrorCollection();

        var brand = await _carBrandWriteRepository.Get(request.CarBrandId, cancellationToken);
        var model = await _carModelWriteRepository.Get(request.CarModelId, cancellationToken);
        var version = await _carModelVersionWriteRepository.Get(request.CarModelVersionId, cancellationToken);

        if (brand is null)
        {
            errors += GeneralErrors.NotFound(nameof(CarBrand), request.CarBrandId);
        }

        if (model is null)
        {
            errors += GeneralErrors.NotFound(nameof(CarModel), request.CarModelId);
        }

        if (version is null)
        {
            errors += GeneralErrors.NotFound(nameof(CarModelVersion), request.CarModelVersionId);
        }

        var registrationResult = GetCarRegistration(request.CarRegistrationInformation);
        if (registrationResult.IsError)
        {
            errors += registrationResult.Errors;
        }

        if (errors.HasErrors)
        {
            return errors.ToList();
        }

        return (brand, model, version, registrationResult.Value)!;
    }

    private static ErrorOr<CarRegistration?> GetCarRegistration(CarRegistrationInformation? registrationInformation)
    {
        if (registrationInformation is null)
        {
            return (CarRegistration?)null;
        }

        var registrationOrError = CarRegistration.Create(registrationInformation.LicencePlate,
            registrationInformation.FirstRegistrationDate,
            registrationInformation.RegistrationExpiryDate);
        return registrationOrError;
    }

    private async Task<ErrorOr<T>> FetchObjectOrReturnNotFoundError<T>(Task<T> task, object id,
        CancellationToken cancellationToken)
    {
        var result = await task;
        if (result == null)
        {
            return GeneralErrors.NotFound(nameof(CarModelVersion), id);
        }

        return result;
    }
}