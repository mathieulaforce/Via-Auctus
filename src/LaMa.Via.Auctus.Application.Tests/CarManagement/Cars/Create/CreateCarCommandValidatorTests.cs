using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Create;
using LaMa.Via.Auctus.Application.CarManagement.Cars.Create;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.Cars.Create;

public class CreateCarCommandValidatorTests
{
    [Fact]
    public void GivenInvalidCommandWhenValidatingReturnsErrors()
    {
        var validator = new CreateCarCommandValidator();
        var empty = Guid.Empty;
        var result = validator.Validate(new CreateCarCommand(CarBrandId.Create(empty),CarModelId.Create(empty),CarModelVersionId.Create(empty), EngineId.Create(empty),null   ));
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(4);
        result.Errors.Select(x => x.PropertyName).Should()
            .ContainInOrder("CarBrandId.Value", "CarModelId.Value", "CarModelVersionId.Value", "EngineId.Value");
    }

    [Fact]
    public void GivenValidCommandWhenValidatingReturnsValid()
    {
        var validator = new CreateCarCommandValidator();
        var random = Guid.NewGuid();
        var result = validator.Validate(new CreateCarCommand(CarBrandId.Create(random),CarModelId.Create(random),CarModelVersionId.Create(random), EngineId.Create(random),new CarRegistrationInformation("abc", DateOnly.MaxValue, DateOnly.MaxValue)   ));
        result.IsValid.Should().BeTrue();
    }
}