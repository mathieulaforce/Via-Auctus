using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.CarManagement;

// We treat fuel type as an entity, because we don't want to manage an enum ourself. (eg: hybrid, electric,... what will the future bring? 
public class FuelType : Entity<string>
{
    private FuelType(string type) : base(type)
    {
    }

    public static FuelType Create(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentNullException(nameof(FuelType));
        }

        return new FuelType(type);
    }
}