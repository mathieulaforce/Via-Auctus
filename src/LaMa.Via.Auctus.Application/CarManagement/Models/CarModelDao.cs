namespace LaMa.Via.Auctus.Application.CarManagement.Models;

public class CarModelDao
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public CarBrandDao Brand { get; init; }
    public Guid BrandId { get; init; }

    public ICollection<CarModelVersionDao> Versions { get; init; }
}