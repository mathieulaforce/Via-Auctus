using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarRegistration
{
    private CarRegistration()
    {
    }

    private CarRegistration(string licensePlate, DateOnly firstRegistrationDate, DateOnly registrationExpiryDate)
    {
        LicensePlate = licensePlate;
        FirstRegistrationDate = firstRegistrationDate;
        RegistrationExpiryDate = registrationExpiryDate;
    }

    public string LicensePlate { get; private set; }
    public DateOnly FirstRegistrationDate { get; private set; }
    public DateOnly RegistrationExpiryDate { get; private set; }

    public static ErrorOr<CarRegistration> Create(string licensePlate, DateOnly firstRegistrationDate,
        DateOnly registrationExpiryDate)
    {
        var errors = ValidateCarRegistration(licensePlate, firstRegistrationDate, registrationExpiryDate);

        if (errors.HasErrors)
        {
            return errors.ToList();
        }

        return new CarRegistration(licensePlate, firstRegistrationDate, registrationExpiryDate);
    }

    private static ErrorCollection ValidateCarRegistration(string licensePlate, DateOnly firstRegistrationDate,
        DateOnly registrationExpiryDate)
    {
        var errors = new ErrorCollection();
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            errors += CarRegistrationErrors.EmptyLicensePlate();
        }

        if (firstRegistrationDate >= registrationExpiryDate)
        {
            errors += CarRegistrationErrors.FirstRegistrationDateAfterExpiryDate(firstRegistrationDate,
                registrationExpiryDate);
        }

        return errors;
    }
}