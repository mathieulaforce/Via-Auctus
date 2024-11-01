using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class CarRegistrationTests
{
    [Fact]
    public void GivenInvalidRegistrationDatesShouldThrowException()
    {
        var action =() => CarRegistration.Create("1abc", new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01));
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void GivenEmptyLicensePlateShouldThrowException()
    {
        var action =() => CarRegistration.Create(string.Empty, new DateOnly(2020, 01, 01), new DateOnly(2024, 01, 01));
        action.Should().Throw<ArgumentException>();
    }
}