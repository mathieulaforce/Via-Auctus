using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

internal class CarBrandWriteRepository : WriteRepository<CarBrand, CarBrandId>, ICarBrandWriteRepository
{
    public CarBrandWriteRepository(ApplicationWriteDbContext context) : base(context)
    {
    }
 
    public async Task<CarBrand?> FindByName(string name, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(brand => brand.Name == name, cancellationToken);
    }
}