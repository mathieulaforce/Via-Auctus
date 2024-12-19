using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.Cars.GetCar;

public record GetCarQuery : IQuery<CarDao>
{
    public Guid CarId { get; init; }
}