namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarRegistration
{
    private CarRegistration(string licensePlate, DateOnly firstRegistrationDate, DateOnly registrationExpiryDate)
    {
        LicensePlate = licensePlate;
        FirstRegistrationDate = firstRegistrationDate;
        RegistrationExpiryDate = registrationExpiryDate;
    }

    public string LicensePlate { get; private set; }
    public DateOnly FirstRegistrationDate { get; private set; }
    public DateOnly RegistrationExpiryDate { get; private set; }

    public static CarRegistration Create(string licensePlate, DateOnly firstRegistrationDate,
        DateOnly registrationExpiryDate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            throw new ArgumentNullException(nameof(licensePlate));
        }

        if (firstRegistrationDate >= registrationExpiryDate)
        {
            throw new ArgumentException("First registration date must be greater than registration expiry");
        }

        return new CarRegistration(licensePlate, firstRegistrationDate, registrationExpiryDate);
    }
}