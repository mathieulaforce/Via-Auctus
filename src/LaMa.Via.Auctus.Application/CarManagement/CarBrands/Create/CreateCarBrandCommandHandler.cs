using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Create;

public class CreateCarBrandCommandHandler(ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCarBrandCommand, CarBrandId>
{
    public async Task<ErrorOr<CarBrandId>> Handle(CreateCarBrandCommand request, CancellationToken cancellationToken)
    {
        var result = await ValidateAndBuildValues(request, cancellationToken);
        if (result.IsError)
        {
            return result.Errors;
        }
        
        var brand = result.Value;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return brand.Id;
    }

    private async Task<ErrorOr<CarBrand>> ValidateAndBuildValues(CreateCarBrandCommand request,
        CancellationToken cancellationToken)
    {
        var errors = new ErrorCollection();
        var theme = CarBrandTheme.Create(request.PrimaryColor, request.SecondaryColor, request.FontFamily,
            request.LogoSvgUrl);
        if (theme.IsError)
        {
            errors += theme.Errors;
        }

        var existingBrand = await carBrandRepository.FindByName(request.Name, cancellationToken);
        if (existingBrand is not null)
        {
            errors += CarBrandErrors.BrandAlreadyExists(existingBrand.Id,request.Name);
           
        }

        if (errors.HasErrors)
        {
            return errors.ToList();
        }

        var brand = CarBrand.Create(request.Name, theme.Value);
        return brand;
    }
}