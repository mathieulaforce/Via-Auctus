using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Domain.CarManagement;

// We treat fuel type as an entity, because we don't want to manage an enum ourself. (eg: hybrid, electric,... what will the future bring? 
public class FuelType : Entity<string>
{
    private FuelType(string type) : base(type)
    {
    }

    public string Value => Id;

    public static ErrorOr<FuelType> Create(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            return FuelTypeErrors.EmptyFuelType();
        }

        return new FuelType(type);
    }
}