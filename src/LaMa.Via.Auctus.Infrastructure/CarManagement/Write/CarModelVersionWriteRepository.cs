using LaMa.Via.Auctus.Application.CarManagement.CarModelVersions;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Write;

internal class CarModelVersionWriteRepository : WriteRepository<CarModelVersion, CarModelVersionId>,
    ICarModelVersionWriteRepository
{
    public CarModelVersionWriteRepository(ApplicationWriteDbContext context) : base(context)
    {
    }
}