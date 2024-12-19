using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarModels.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels.GetCarModelsByBrand;

public record CarModelsByBrandQuery : IQuery<IReadOnlyCollection<CarModelVM>>
{
    public Guid CarBrandId { get; init; }
}