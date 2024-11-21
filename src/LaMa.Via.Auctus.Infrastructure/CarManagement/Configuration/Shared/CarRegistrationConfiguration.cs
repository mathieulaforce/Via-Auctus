using LaMa.Via.Auctus.Domain.CarManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Shared;

public static class CarRegistrationConfiguration
{
    internal static void RegisterCarRegistrationValueObject<T>(
        this OwnedNavigationBuilder<T, CarRegistration> registrationBuilder) where T : class
    {
        registrationBuilder.Property(registration => registration.LicensePlate)
            .HasMaxLength(30);
        registrationBuilder.Property(registration => registration.FirstRegistrationDate);
        registrationBuilder.Property(registration => registration.RegistrationExpiryDate);
    }
}