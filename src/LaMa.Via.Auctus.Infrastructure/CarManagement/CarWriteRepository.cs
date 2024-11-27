using LaMa.Via.Auctus.Application.CarManagement.Cars;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions; 

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

internal class CarWriteRepository : WriteRepository<Car, CarId>, ICarWriteRepository
{
    public CarWriteRepository(ApplicationWriteDbContext context) : base(context)
    {
    }
 
}