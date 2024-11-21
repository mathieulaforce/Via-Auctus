using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Update;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.CarBrands.Update;

public class UpdateCarBrandCommandHandlerTests
{
    private readonly ICarBrandWriteRepository _carBrandWriteRepository;
    private readonly ICommandHandler<UpdateCarBrandCommand, CarBrandId> _sut;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCarBrandCommandHandlerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _carBrandWriteRepository = A.Fake<ICarBrandWriteRepository>();
        _sut = new UpdateCarBrandCommandHandler(_carBrandWriteRepository, _unitOfWork);
    }

    [Fact]
    public async Task GivenExistingBrandWhenHandleThenUpdateBrand()
    {
        var tesla = CarBrandObjectMother.Tesla();
        A.CallTo(() => _carBrandWriteRepository.Get(tesla.Id, default)).Returns(tesla);
        A.CallTo(() => _carBrandWriteRepository.FindByName("TEST", default)).Returns((CarBrand?)null);

        var command = new UpdateCarBrandCommand(tesla.Id, "TEST", "#FFF", "#AAA", "Some random font", "test.svg");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeFalse();
        result.Value.Should().Be(tesla.Id);
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.FindByName("TEST", default)).MustHaveHappened();
        tesla.Name.Should().Be("TEST");
        tesla.Theme.FontFamily.Should().Be("Some random font");
        tesla.Theme.PrimaryColor.Code.Should().Be("#FFF");
        tesla.Theme.SecondaryColor!.Code.Should().Be("#AAA");
        tesla.Theme.Logo.Url.Should().Be("test.svg");
    }
    
    [Fact]
    public async Task GivenNonExistingCarBrandWhenHandleThenReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla();
        A.CallTo(() => _carBrandWriteRepository.Get(tesla.Id, default)).Returns((CarBrand?)null);
        A.CallTo(() => _carBrandWriteRepository.FindByName("audi", default)).Returns(CarBrandObjectMother.Audi());

        var command = new UpdateCarBrandCommand(tesla.Id, "audi", "#FFF", "#AAA", "Some random font", "test.svg");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarBrandErrors.BrandNotFound(tesla.Id).Code);
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustNotHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.FindByName("audi", default)).MustHaveHappened(); 
    }
     
    [Fact]
    public async Task GivenBrandWithSameNameWhenHandleThenReturnsError()
    {
        var tesla = CarBrandObjectMother.Tesla();
        A.CallTo(() => _carBrandWriteRepository.Get(tesla.Id, default)).Returns(tesla);
        A.CallTo(() => _carBrandWriteRepository.FindByName("audi", default)).Returns(CarBrandObjectMother.Audi());

        var command = new UpdateCarBrandCommand(tesla.Id, "audi", "#FFF", "#AAA", "Some random font", "test.svg");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(CarBrandErrors.BrandAlreadyExists(tesla.Id, "audi").Code);
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustNotHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.FindByName("audi", default)).MustHaveHappened(); 
    }
}