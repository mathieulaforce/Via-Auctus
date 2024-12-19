using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels.Models;

public class CarModelVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CarBrandSummary Brand { get; set; }
    public string ImageUrl { get; set; }
    public IReadOnlyCollection<CarVersionVM> Versions { get; set; }
}

public class CarVersionVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}