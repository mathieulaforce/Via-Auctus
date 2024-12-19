using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Lookup;

public record LookupCarBrandQuery : IQuery<IReadOnlyCollection<CarBrandSummary>>
{
}