using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Create;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
using LaMa.Via.Auctus.Domain.Shared.Errors;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.CarBrands.Create;

public class CreateCarModelCommandHandlerTests
{
    private readonly ICarBrandWriteRepository _carBrandWriteRepository;
    private readonly ICommandHandler<CreateCarBrandCommand, CarBrandId> _sut;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarModelCommandHandlerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _carBrandWriteRepository = A.Fake<ICarBrandWriteRepository>();
        _sut = new CreateCarBrandCommandHandler(_carBrandWriteRepository, _unitOfWork);
    }

    [Fact]
    public async Task GivenValidCommandWhenHandleThenCreateBrand()
    {
        A.CallTo(() => _carBrandWriteRepository.FindByName("Tesla", default)).Returns((CarBrand?)null);

        var command = new CreateCarBrandCommand("Tesla", "#CC0000", "#333333", "Roboto, sans-serif", "tesla_logo.svg");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<CarBrandId>().Subject.Value.Should().NotBeEmpty();
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.FindByName("Tesla", default)).MustHaveHappened();
    }

    [Fact]
    public async Task GivenInvalidThemeWhenHandleThenReturnsError()
    {
        A.CallTo(() => _carBrandWriteRepository.FindByName("Tesla", default)).Returns((CarBrand?)null);

        var command = new CreateCarBrandCommand("Tesla", "green", "#333333", "Roboto, sans-serif", "tesla_logo.svg");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CssColorErrors.InvalidColorCode().Code);
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustNotHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.FindByName("Tesla", default)).MustHaveHappened();
    }

    [Fact]
    public async Task GivenExistingBrandWhenHandleThenReturnError()
    {
        var existingBrand = CarBrandObjectMother.Tesla;
        A.CallTo(() => _carBrandWriteRepository.FindByName("Tesla", default)).Returns(existingBrand);

        var command = new CreateCarBrandCommand("Tesla", "#CC0000", "#333333", "Roboto, sans-serif", "tesla_logo.svg");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(e =>
            e.Code == CarBrandErrors.BrandAlreadyExists(existingBrand.Id, existingBrand.Name).Code);
    }
}