using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars;

public interface ICarWriteRepository : IWriteRepository
{
    Task<Car?> Get(CarId id, CancellationToken cancellationToken = default);
    Task Add(Car car, CancellationToken cancellationToken = default);
}