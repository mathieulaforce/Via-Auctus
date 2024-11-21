using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class CarRegistrationTests
{
    [Fact]
    public void GivenInvalidRegistrationDatesShouldContainFirstRegistrationDateAfterExpiryDateError()
    {
        var firstRegistration = new DateOnly(2020, 01, 01);
        var registrationExpiry = new DateOnly(2020, 01, 01);
        var registration = CarRegistration.Create("1abc", firstRegistration, registrationExpiry);
        registration.IsError.Should().BeTrue();
        registration.Errors.Should().ContainSingle().Which.Code.Should().Be(CarRegistrationErrors
            .FirstRegistrationDateAfterExpiryDate(firstRegistration, registrationExpiry).Code);
    }

    [Fact]
    public void GivenEmptyLicensePlateShouldContainEmptyLicensePlateError()
    {
        var firstRegistration = new DateOnly(2020, 01, 01);
        var registrationExpiry = new DateOnly(2024, 01, 01);
        var registration = CarRegistration.Create(string.Empty, firstRegistration, registrationExpiry);
        registration.IsError.Should().BeTrue();
        registration.Errors.Should().ContainSingle().Which.Code.Should()
            .Be(CarRegistrationErrors.EmptyLicensePlate().Code);
    }

    [Fact]
    public void GivenEmptyLicensePlateAndWrongRegistrationDatesShouldContain2Errors()
    {
        var firstRegistration = new DateOnly(2020, 01, 01);
        var registrationExpiry = new DateOnly(2020, 01, 01);
        var registration = CarRegistration.Create(string.Empty, firstRegistration, registrationExpiry);
        registration.IsError.Should().BeTrue();
        registration.Errors.Should().HaveCount(2);
    }
}