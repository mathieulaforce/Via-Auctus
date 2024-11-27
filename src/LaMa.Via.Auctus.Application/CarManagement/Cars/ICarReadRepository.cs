using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars;

public interface ICarReadRepository : IReadRepository
{
    Task<CarDao?> Get(Guid id, CancellationToken cancellationToken = default);
}