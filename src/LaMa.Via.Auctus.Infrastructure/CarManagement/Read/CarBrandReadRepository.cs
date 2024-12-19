using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Models;
using LaMa.Via.Auctus.Application.CarManagement.Models;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Read;

internal class CarBrandReadRepository : ReadRepository<CarBrandDao>, ICarBrandReadRepository
{
    public CarBrandReadRepository(ApplicationReadDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyCollection<CarBrandSummary>> Lookup(CancellationToken cancellationToken = default)
    {
        var brands = await Entity.Select(brand => brand.MapToSummary()).ToListAsync(cancellationToken);
        return brands.AsReadOnly();
    }

    public async Task<CarBrandDao?> Get(CarBrandId id, CancellationToken cancellationToken = default)
    {
        return await Entity.FindAsync([id.Value], cancellationToken);
    }
}