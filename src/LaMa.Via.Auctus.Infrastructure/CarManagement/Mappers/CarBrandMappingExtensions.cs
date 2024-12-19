using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Models;
using LaMa.Via.Auctus.Application.CarManagement.Models;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Mappers;

public static class CarBrandMappingExtensions
{
    public static CarBrandSummary MapToSummary(this CarBrandDao carBrand)
    {
        return new CarBrandSummary
        {
            Id = carBrand.Id,
            Name = carBrand.Name
        };
    }
}