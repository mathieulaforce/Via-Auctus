using LaMa.Via.Auctus.Application.CarManagement.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars;

public interface ICarReadRepository
{
    Task<CarDao?> Get(Guid id, CancellationToken cancellationToken = default);
}