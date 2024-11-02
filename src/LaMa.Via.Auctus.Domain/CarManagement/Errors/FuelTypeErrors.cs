using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public class FuelTypeErrors
{
    public static Error EmptyFuelType()
    {
        return Error.Validation(
            "FuelType.EmptyFuelType",
            "Fuel type cannot be empty."
        );
    }
}