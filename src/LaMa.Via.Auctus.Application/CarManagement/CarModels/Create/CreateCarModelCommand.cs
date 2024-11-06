using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels.Create;

public record CreateCarModelCommand(string Name, CarBrandId CarBrandId, string? ImageUrl = null): ICommand<CarModelId>
{
 
}