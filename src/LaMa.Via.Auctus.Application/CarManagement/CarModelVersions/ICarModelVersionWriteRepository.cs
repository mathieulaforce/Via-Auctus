using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModelVersions;

public interface ICarModelVersionWriteRepository: IWriteRepository
{
    Task<CarModelVersion?> Get(CarModelVersionId id, CancellationToken cancellationToken = default);
    Task Add(CarModelVersion carModelVersion, CancellationToken cancellationToken = default);
}