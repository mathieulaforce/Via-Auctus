using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Domain.CarManagement;
public class FuelType : Entity<string>
{
    private FuelType(): base() { }
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