using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Update;

internal class UpdateCarBrandCommandHandler(ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCarBrandCommand, CarBrandId>
{
    public async Task<ErrorOr<CarBrandId>> Handle(UpdateCarBrandCommand request, CancellationToken cancellationToken)
    {
        var result = await ValidateAndGetExistingBrand(request, cancellationToken);
        if (result.IsError)
        {
            return result.Errors;
        }
        
        var (brand, theme) = result.Value;
        brand.UpdateTheme(theme);
        brand.ChangeName(request.Name);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return brand.Id;
    }

    private async Task<ErrorOr<(CarBrand, CarBrandTheme)>> ValidateAndGetExistingBrand(UpdateCarBrandCommand request,
        CancellationToken cancellationToken)
    {
        var errors = new ErrorCollection();
        
        var brand = await carBrandRepository.Get(request.Id, cancellationToken);
        var brandWithSameName = await carBrandRepository.FindByName(request.Name, cancellationToken);
        if (brand is null)
        {
            errors += CarBrandErrors.BrandNotFound(request.Id);
            return errors.ToList();
        }
        
        if (brandWithSameName is not null)
        {
            errors += CarBrandErrors.BrandAlreadyExists(brandWithSameName.Id, brandWithSameName.Name);
            return errors.ToList();
        }
        
        var theme = CarBrandTheme.Create(request.PrimaryColor, request.SecondaryColor, request.FontFamily,
            request.LogoSvgUrl);
        
        if (theme.IsError)
        {
            errors += theme.Errors;
            return errors.ToList();
        } 
        
        return (brand, theme.Value);
    }
}