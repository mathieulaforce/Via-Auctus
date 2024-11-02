using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands;

public interface ICarBrandRepository
{
    Task<CarBrand?> Get(CarId id, CancellationToken cancellationToken = default);
    Task<CarBrand?> FindByName(string name, CancellationToken cancellationToken = default);
    void Add(CarBrand carBrand);
}