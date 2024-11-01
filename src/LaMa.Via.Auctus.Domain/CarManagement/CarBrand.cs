using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public class CarBrandId : ValueObject
{
    private CarBrandId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Value;
    }

    public static CarBrandId CreateUnique()
    {
        return new CarBrandId(UniqueIdGenerator.Generate());
    }

    public static CarBrandId Create(Guid value)
    {
        return new CarBrandId(value);
    }
}

public class CarBrand : Entity<CarBrandId>
{
    private CarBrand(CarBrandId id, string name, CarBrandTheme theme) : base(id)
    {
        Name = name;
        Theme = theme;
    }

    public string Name { get; private set; }

    public CarBrandTheme Theme { get; private set; }

    public static CarBrand Create(string name, CarBrandTheme theme)
    {
        var id = CarBrandId.CreateUnique();
        return new CarBrand(id, name, theme);
    }

    public void UpdateTheme(CarBrandTheme theme)
    {
        Theme = theme;
    }
}