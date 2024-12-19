using LaMa.Via.Auctus.Application.CarManagement.CarModels;
using LaMa.Via.Auctus.Application.CarManagement.CarModels.Models;
using LaMa.Via.Auctus.Application.CarManagement.Models;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Read;

internal class CarModelReadRepository : ReadRepository<CarModelDao>, ICarModelReadRepository
{
    public CarModelReadRepository(ApplicationReadDbContext context) : base(context)
    {
    }


    public async Task<IReadOnlyCollection<CarModelVM>> Get(CarBrandId brandId,
        CancellationToken cancellationToken = default)
    {
        return await Entity.Include(carModel => carModel.Versions)
            .Where(carModel => carModel.BrandId == brandId.Value)
            .OrderBy(carModel => carModel.Name)
            .Select(carModel => new CarModelVM
            {
                Id = carModel.Id,
                Name = carModel.Name,
                Brand = carModel.Brand.MapToSummary(),
                ImageUrl = carModel.ImageUrl,
                Versions = carModel.Versions.Select(version => new CarVersionVM
                {
                    Id = version.Id,
                    Name = version.Name,
                    Year = version.Year
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
}