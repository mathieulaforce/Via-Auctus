using LaMa.Via.Auctus.Application.CarManagement.Cars;
using LaMa.Via.Auctus.Application.CarManagement.Models;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

internal class CarReadRepository : ReadRepository<CarDao>, ICarReadRepository
{
    public CarReadRepository(ApplicationReadDbContext context) : base(context)
    {
    }

    public async Task<CarDao?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entity
            .Include(e => e.Brand)
            .Include(e => e.Model)
            .Include(e => e.Version)
            .Include(e => e.Engine)
            .FirstOrDefaultAsync(car => car.Id == id, cancellationToken);
    }
}