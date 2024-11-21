using LaMa.Via.Auctus.Application.CarManagement.Cars;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

public class CarWriteRepository : WriteRepository<Car>, ICarWriteRepository
{
    public CarWriteRepository(ApplicationWriteDbContext context) : base(context)
    {
    }

    public async Task<Car?> Get(CarId id, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(car => car.Id == id, cancellationToken);
    }
}