using LaMa.Via.Auctus.Application.CarManagement.CarModelVersions;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

public class CarModelVersionWriteRepository : WriteRepository<CarModelVersion>, ICarModelVersionWriteRepository
{
    public CarModelVersionWriteRepository(ApplicationWriteDbContext context) : base(context)
    {
    }

    public async Task<CarModelVersion?> Get(CarModelVersionId id, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(model => model.Id == id, cancellationToken);
    }
}