using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Update;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.CarBrands.Update;

public class UpdateCarBrandCommandValidatorTests
{
    [Fact]
    public void GivenInvalidCommandWhenValidatingReturnsErrors()
    {
        var validator = new UpdateCarBrandCommandValidator();
        var result = validator.Validate(new UpdateCarBrandCommand(CarBrandId.Create(Guid.Empty), "", "", null, "", ""));
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(5);
        result.Errors.Select(x => x.PropertyName).Should()
            .ContainInOrder("Id.Value", "Name", "PrimaryColor", "FontFamily", "LogoSvgUrl");
    }

    [Fact]
    public void GivenValidCommandWhenValidatingReturnsValid()
    {
        var validator = new UpdateCarBrandCommandValidator();
        var result =
            validator.Validate(new UpdateCarBrandCommand(CarBrandId.CreateUnique(), "name", "primary", null, "font",
                "logo"));
        result.IsValid.Should().BeTrue();
    }
}