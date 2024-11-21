using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars;

public interface ICarWriteRepository
{
    Task<Car?> Get(CarId id, CancellationToken cancellationToken = default);
    Task Add(Car car, CancellationToken cancellationToken = default);
}