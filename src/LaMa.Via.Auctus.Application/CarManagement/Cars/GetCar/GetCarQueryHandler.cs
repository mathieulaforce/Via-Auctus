using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars.GetCar;

internal class GetCarQueryHandler(ICarReadRepository carReadRepository) : IQueryHandler<GetCarQuery, CarDao>
{
    public async Task<ErrorOr<CarDao>> Handle(GetCarQuery request, CancellationToken cancellationToken)
    {
        return await carReadRepository.Get(request.CarId, cancellationToken);
    }
}