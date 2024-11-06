using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarModelVersionId 
{
    private CarModelVersionId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; private set; }

    public static CarModelVersionId CreateUnique()
    {
        return new CarModelVersionId(Guid.NewGuid());
    }

    public static CarModelVersionId Create(Guid value)
    {
        return new CarModelVersionId(value);
    }
}

public sealed class CarModelVersion : AggregateRoot<CarModelVersionId>
{
    private CarModelVersion():base() {}
    private CarModelVersion(CarModelVersionId id, CarModelId carModelId, string name, int year,
        Engines engines,
        SupportedImage? image) : base(id)
    {
        CarModelId = carModelId;
        Name = name;
        Year = year;
        Image = image;
        Engines = engines;
    }

    public CarModelId CarModelId { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public SupportedImage? Image { get; set; }
    public Engines Engines { get; set; }

    public static CarModelVersion Create(CarModelId carModelId, string versionName, int year, Engines engines,
        SupportedImage? image = null)
    {
        var id = CarModelVersionId.CreateUnique();
        return new CarModelVersion(id, carModelId, versionName, year, engines, image);
    }

    public bool HasEngine(Engine engine)
    {
        return Engines.Contains(engine);
    }
}