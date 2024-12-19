using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarModels.Models;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels;

public interface ICarModelReadRepository : IReadRepository
{
    Task<IReadOnlyCollection<CarModelVM>> Get(CarBrandId brandId, CancellationToken cancellationToken = default);
}