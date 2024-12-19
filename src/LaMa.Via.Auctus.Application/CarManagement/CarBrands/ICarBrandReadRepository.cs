using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Models;
using LaMa.Via.Auctus.Application.CarManagement.Models;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands;

public interface ICarBrandReadRepository : IReadRepository
{
    Task<IReadOnlyCollection<CarBrandSummary>> Lookup(CancellationToken cancellationToken = default);
    Task<CarBrandDao?> Get(CarBrandId id, CancellationToken cancellationToken = default);
}