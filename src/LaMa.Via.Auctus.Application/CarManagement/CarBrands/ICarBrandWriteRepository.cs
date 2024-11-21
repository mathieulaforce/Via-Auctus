using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands;

public interface ICarBrandWriteRepository
{
    Task<CarBrand?> Get(CarBrandId id, CancellationToken cancellationToken = default);
    Task<CarBrand?> FindByName(string name, CancellationToken cancellationToken = default);
    Task Add(CarBrand carBrand, CancellationToken cancellationToken = default);
}