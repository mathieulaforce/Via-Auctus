﻿using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Events;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarBrandId
{
    private CarBrandId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

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
    private CarBrand()
    {
    }

    private CarBrand(CarBrandId id, string name, CarBrandTheme theme) : base(id)
    {
        Name = name;
        Theme = theme;
    }

    public string Name { get; private set; } = null!;

    public CarBrandTheme Theme { get; private set; } = null!;

    public static CarBrand Create(string name, CarBrandTheme theme)
    {
        var id = CarBrandId.CreateUnique();
        var brand = new CarBrand(id, name, theme);
        brand.RaiseDomainEvent(new CarBrandCreatedDomainEvent(brand.Id));
        return brand;
    }

    public void UpdateTheme(CarBrandTheme theme)
    {
        Theme = theme;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
        {
            return;
        }

        Name = name;
    }

    public bool SupportsModel(CarModel model)
    {
        return model.CarBrandId == Id;
    }
}