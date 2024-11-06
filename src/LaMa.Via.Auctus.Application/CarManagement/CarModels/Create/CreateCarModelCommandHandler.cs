using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels.Create;

public class CreateCarModelCommandHandler : ICommandHandler<CreateCarModelCommand, CarModelId>
{
    private readonly ICarModelRepository _carModelRepository;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarModelCommandHandler(ICarModelRepository carModelRepository, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _carModelRepository = carModelRepository;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<CarModelId>> Handle(CreateCarModelCommand request, CancellationToken cancellationToken)
    {
        var model = await ValidateAndBuildValues(request, cancellationToken);
        if (model.IsError)
        {
            return model.Errors;
        }
        await _carModelRepository.Add(model.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return model.Value.Id;
    }
    
    private async Task<ErrorOr<CarModel>> ValidateAndBuildValues(CreateCarModelCommand request,
        CancellationToken cancellationToken)
    {
        var errors = new ErrorCollection();
        
        ErrorOr<SupportedImage?> supportedImage = string.IsNullOrWhiteSpace(request.ImageUrl) ? (SupportedImage?)null :SupportedImage.Create(request.ImageUrl) ;
        if (supportedImage.IsError)
        {
            errors+= supportedImage.Errors;
        }
         
        var brand = await _carBrandRepository.Get(request.CarBrandId, cancellationToken);
        var existingModel = await _carModelRepository.FindByName(request.Name, cancellationToken);
        if (existingModel is not null)
        {
            errors += CarModelErrors.ModelAlreadyExists(existingModel.Id,request.Name); 
        }

        if (brand is null)
        {
            errors += CarBrandErrors.BrandNotFound(request.CarBrandId);
        }

        if (errors.HasErrors)
        {
            return errors.ToList();
        }

        var model = CarModel.Create(request.Name, brand!.Id, supportedImage.Value);
        return model;
    }
}