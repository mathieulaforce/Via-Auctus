using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Create;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.CarBrands.Create;

public class CreateCarBrandCommandValidatorTests
{
    [Fact]
    public void GivenInvalidCommandWhenValidatingReturnsErrors()
    {
        var validator = new CreateCarBrandCommandValidator();
        var result = validator.Validate(new CreateCarBrandCommand("", "", null, "", ""));
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(4);
        result.Errors.Select(x => x.PropertyName).Should()
            .ContainInOrder("Name", "PrimaryColor", "FontFamily", "LogoSvgUrl");
    }
    
    [Fact]
    public void GivenValidCommandWhenValidatingReturnsValid()
    {
        var validator = new CreateCarBrandCommandValidator();
        var result = validator.Validate(new CreateCarBrandCommand("name", "primary", null, "font", "logo"));
        result.IsValid.Should().BeTrue(); 
    }
}