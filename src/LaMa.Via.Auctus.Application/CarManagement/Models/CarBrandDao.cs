namespace LaMa.Via.Auctus.Application.CarManagement.Models;

public class CarBrandDao
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public ICollection<CarModelDao> Models { get; set; }
}