using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Update;

public sealed record UpdateCarBrandCommand(
    CarBrandId Id,
    string Name,
    string PrimaryColor,
    string? SecondaryColor,
    string FontFamily,
    string LogoSvgUrl) : ICommand<CarBrandId>
{
}