using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarModelId
{
    private CarModelId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }
 
    public static CarModelId CreateUnique()
    {
        return new CarModelId(UniqueIdGenerator.Generate());
    }

    public static CarModelId Create(Guid value)
    {
        return new CarModelId(value);
    }
}

public class CarModel : Entity<CarModelId>
{
    private CarModel(CarModelId id, string name, CarBrandId carBrandId, SupportedImage? image) : base(id)
    {
        Name = name;
        CarBrandId = carBrandId;
        Image = image;
    }

    public string Name { get; private set; }
    public CarBrandId CarBrandId { get; private set; }
    public SupportedImage? Image { get; private set; }

    public static CarModel Create(string name, CarBrandId carBrandId, SupportedImage? image)
    {
        var id = CarModelId.CreateUnique();
        return new CarModel(id, name, carBrandId, image);
    }
}