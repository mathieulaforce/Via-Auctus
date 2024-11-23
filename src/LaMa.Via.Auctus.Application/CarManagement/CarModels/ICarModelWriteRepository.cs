using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels;

public interface ICarModelWriteRepository
{
    Task<CarModel?> Get(CarModelId id, CancellationToken cancellationToken = default);
    Task<CarModel?> FindByName(string name, CarBrandId brandId, CancellationToken cancellationToken = default);
    Task Add(CarModel carModel, CancellationToken cancellationToken = default);
}