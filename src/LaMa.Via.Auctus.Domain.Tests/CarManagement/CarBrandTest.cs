using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class CarBrandTest
{
    [Fact]
    public void GivenBrandWhenUpdateThenCarBrandIsUpdated()
    {
        var brand = CarBrandObjectMother.CreateNewFrom(CarBrandObjectMother.Audi);

        brand.UpdateTheme(CarBrandObjectMother.Tesla.Theme);
        brand.ChangeName("Test");

        brand.Name.Should().Be("Test");
        brand.Theme.Should().Be(CarBrandObjectMother.Tesla.Theme);
        brand.Theme.Logo.Should().Be(CarBrandObjectMother.Tesla.Theme.Logo);
        brand.Theme.FontFamily.Should().Be(CarBrandObjectMother.Tesla.Theme.FontFamily);
        brand.Theme.PrimaryColor.Should().Be(CarBrandObjectMother.Tesla.Theme.PrimaryColor);
        brand.Theme.SecondaryColor.Should().Be(CarBrandObjectMother.Tesla.Theme.SecondaryColor);
        brand.Theme.Should().NotBe(CarBrandObjectMother.Audi.Theme);
    }

    [Fact]
    public void GivenEmptyBrandNameWhenUpdateThenThrows()
    {
        var brand = CarBrandObjectMother.CreateNewFrom(CarBrandObjectMother.Audi);

        Assert.Throws<ArgumentNullException>(() => brand.ChangeName(""));
    }
}