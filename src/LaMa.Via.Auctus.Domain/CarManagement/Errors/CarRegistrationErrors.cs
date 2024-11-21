using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public static class CarRegistrationErrors
{
    public static Error EmptyLicensePlate()
    {
        return Error.Validation(
            "CarRegistration.EmptyLicensePlate",
            "License Plate is empty."
        );
    }

    public static Error FirstRegistrationDateAfterExpiryDate(DateOnly firstRegistrationDate, DateOnly expiryDate)
    {
        return Error.Validation(
            "CarRegistration.InvalidRegistrationDates",
            "First registration date must be greater than registration expiry",
            new Dictionary<string, object>
            {
                { "firstRegistration", firstRegistrationDate },
                { "registrationExpiry", expiryDate }
            }
        );
    }
}