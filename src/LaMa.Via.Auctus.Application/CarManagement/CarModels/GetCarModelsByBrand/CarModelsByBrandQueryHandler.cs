using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarModels.Models;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels.GetCarModelsByBrand;

internal class CarModelsByBrandQueryHandler : IQueryHandler<CarModelsByBrandQuery, IReadOnlyCollection<CarModelVM>>
{
    private readonly ICarBrandReadRepository _carBrandReadRepository;
    private readonly ICarModelReadRepository _carModelReadRepository;

    public CarModelsByBrandQueryHandler(ICarBrandReadRepository carBrandReadRepository,
        ICarModelReadRepository carModelReadRepository)
    {
        _carBrandReadRepository = carBrandReadRepository;
        _carModelReadRepository = carModelReadRepository;
    }

    public async Task<ErrorOr<IReadOnlyCollection<CarModelVM>>> Handle(CarModelsByBrandQuery request,
        CancellationToken cancellationToken)
    {
        var brandId = CarBrandId.Create(request.CarBrandId);
        var brand = await _carBrandReadRepository.Get(brandId);
        if (brand == null)
        {
            return CarBrandErrors.BrandNotFound(brandId);
        }

        var models = await _carModelReadRepository.Get(brandId, cancellationToken);
        return ErrorOrFactory.From(models);
    }
}