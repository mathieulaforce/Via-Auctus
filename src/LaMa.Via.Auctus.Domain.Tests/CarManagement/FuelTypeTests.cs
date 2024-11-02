using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class FuelTypeTests
{
    [Fact]
    public void GivenEmptyFuelTypeWhenCreateThenReturnsError()
    {
        var result = FuelType.Create(string.Empty);
        result.IsError.Should().BeTrue();
    }
    
    [Fact]
    public void GivenFuelTypeWhenCreateThenReturnsSuccess()
    {
        var result = FuelType.Create("Electric");
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be("Electric");
        result.Value.Value.Should().Be("Electric");
    }
}