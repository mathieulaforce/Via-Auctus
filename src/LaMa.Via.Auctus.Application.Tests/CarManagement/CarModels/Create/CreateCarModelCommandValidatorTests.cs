using LaMa.Via.Auctus.Application.CarManagement.CarModels.Create;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.CarModels.Create;

public class CreateCarModelsCommandValidatorTests
{
    [Fact]
    public void GivenInvalidCommandWhenValidatingReturnsErrors()
    {
        var validator = new CreateCarModelCommandValidator();
        var result = validator.Validate(new CreateCarModelCommand("", CarBrandId.Create(Guid.Empty)));
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Select(x => x.PropertyName).Should()
            .ContainInOrder("Name", "CarBrandId.Value");
    }

    [Fact]
    public void GivenValidCommandWhenValidatingReturnsValid()
    {
        var validator = new CreateCarModelCommandValidator();
        var result = validator.Validate(new CreateCarModelCommand("Audi", CarBrandId.Create(Guid.NewGuid())));
        result.IsValid.Should().BeTrue();
    }
}