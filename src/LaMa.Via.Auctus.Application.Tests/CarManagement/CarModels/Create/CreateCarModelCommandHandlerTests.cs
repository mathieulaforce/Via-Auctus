using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarModels;
using LaMa.Via.Auctus.Application.CarManagement.CarModels.Create;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared.Errors;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.CarModels.Create;

public class CreateCarModelCommandHandlerTests
{
    private readonly ICarBrandWriteRepository _carBrandWriteRepository;
    private readonly ICarModelWriteRepository _carModelWriteRepository;
    private readonly ICommandHandler<CreateCarModelCommand, CarModelId> _sut;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarModelCommandHandlerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _carModelWriteRepository = A.Fake<ICarModelWriteRepository>();
        _carBrandWriteRepository = A.Fake<ICarBrandWriteRepository>();
        _sut = new CreateCarModelCommandHandler(_carModelWriteRepository, _carBrandWriteRepository, _unitOfWork);
    }

    [Fact]
    public async Task GivenValidCommandWhenHandleThenCreateModel()
    {
        var brand = CarBrandObjectMother.Skoda;
        var modelName = "Enyaq";
        A.CallTo(() => _carBrandWriteRepository.Get(brand.Id, default)).Returns(brand);
        A.CallTo(() => _carModelWriteRepository.FindByName(modelName, brand.Id, default)).Returns(null as CarModel);

        var command = new CreateCarModelCommand(modelName, brand.Id);
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeFalse(string.Join(", ", result.Errors.Select(x => x.Code).ToList()));
        result.Value.Should().BeOfType<CarModelId>().Subject.Value.Should().NotBeEmpty();
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustHaveHappened();
        A.CallTo(() => _carModelWriteRepository.FindByName(modelName, brand.Id, default)).MustHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.Get(brand.Id, default)).MustHaveHappened();
    }

    [Fact]
    public async Task GivenAlreadyExistingModelWhenHandleThenErrors()
    {
        var brand = CarBrandObjectMother.Bmw;
        var existingModel = CarModelObjectMother.BmwX1();
        A.CallTo(() => _carBrandWriteRepository.Get(brand.Id, default)).Returns(brand);
        A.CallTo(() => _carModelWriteRepository.FindByName(existingModel.Name, brand.Id, default))
            .Returns(existingModel);

        var command = new CreateCarModelCommand(existingModel.Name, brand.Id);
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeTrue();
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustNotHaveHappened();
    }

    [Fact]
    public async Task GivenInvalidImageUrldWhenHandleThenErrors()
    {
        var brand = CarBrandObjectMother.Skoda;
        var modelName = "Enyaq";
        A.CallTo(() => _carBrandWriteRepository.Get(brand.Id, default)).Returns(brand);
        A.CallTo(() => _carModelWriteRepository.FindByName(modelName, brand.Id, default)).Returns(null as CarModel);

        var command = new CreateCarModelCommand(modelName, brand.Id, "not an image");
        var result = await _sut.Handle(command, default);

        result.IsError.Should().BeTrue();
        result.Errors.Single().Should().Be(SupportedImageErrors.InvalidExtension());
    }
}