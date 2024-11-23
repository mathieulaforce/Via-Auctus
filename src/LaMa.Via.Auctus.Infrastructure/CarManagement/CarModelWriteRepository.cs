using LaMa.Via.Auctus.Application.CarManagement.CarModels;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

public class CarModelWriteRepository : WriteRepository<CarModel>, ICarModelWriteRepository
{
    public CarModelWriteRepository(ApplicationWriteDbContext context) : base(context)
    {
    }

    public async Task<CarModel?> Get(CarModelId id, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(model => model.Id == id, cancellationToken);
    }

    public async Task<CarModel?> FindByName(string name, CarBrandId brandId, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(model => model.Name == name && model.CarBrandId == brandId, cancellationToken);
    }
}