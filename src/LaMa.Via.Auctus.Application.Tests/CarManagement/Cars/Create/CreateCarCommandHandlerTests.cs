using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Create;
using LaMa.Via.Auctus.Application.CarManagement.CarModels;
using LaMa.Via.Auctus.Application.CarManagement.CarModelVersions;
using LaMa.Via.Auctus.Application.CarManagement.Cars;
using LaMa.Via.Auctus.Application.CarManagement.Cars.Create;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Errors;
using LaMa.Via.Auctus.Domain.Shared.Errors;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.Cars.Create;

public class CreateCarCommandHandlerTests
{
    private readonly ICommandHandler<CreateCarCommand, CarId> _sut;
    private readonly IUnitOfWork _unitOfWork; 
    private readonly ICarWriteRepository _carWriteRepository;
    private readonly ICarBrandWriteRepository _carBrandWriteRepository;
    private readonly ICarModelWriteRepository _carModelWriteRepository;
    private readonly ICarModelVersionWriteRepository _carModelVersionWriteRepository;

    public CreateCarCommandHandlerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _carWriteRepository = A.Fake<ICarWriteRepository>();
        _carBrandWriteRepository = A.Fake<ICarBrandWriteRepository>();
        _carModelWriteRepository = A.Fake<ICarModelWriteRepository>();
        _carModelVersionWriteRepository = A.Fake<ICarModelVersionWriteRepository>();
        _sut = new CreateCarCommandHandler(_carWriteRepository, _carBrandWriteRepository,_carModelWriteRepository,_carModelVersionWriteRepository, _unitOfWork);
    }

    [Fact]
    public async Task GivenCarWithoutRegistrationWhenHandleThenCreateCar()
    {
        var tesla = CarObjectMother.RegisteredTeslaModelYAllWheelDrive();
        var engine = EngineObjectMother.TeslaModel3Motor;
        var engines = new Engines();
        engines.AddEngine(engine);
        
        A.CallTo(() => _carBrandWriteRepository.Get(tesla.BrandId, default)).Returns(CarBrandObjectMother.Tesla());
        A.CallTo(() => _carModelWriteRepository.Get(tesla.ModelId, default)).Returns(CarModelObjectMother.TeslaModelY());
        A.CallTo(() => _carModelVersionWriteRepository.Get(tesla.VersionId, default)).Returns(CarModelVersion.Create(tesla.ModelId, "Long Range All-Wheel Drive", 2024, engines, default));
         
        var command = new CreateCarCommand(tesla.BrandId, tesla.ModelId, tesla.VersionId, tesla.EngineId, null );
        var result = await _sut.Handle(command, default);
        
        result.IsError.Should().BeFalse();
        result.Value.Should().BeOfType<CarBrandId>().Subject.Value.Should().NotBeEmpty();
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustHaveHappened();
        A.CallTo(() => _carBrandWriteRepository.FindByName("Tesla", default)).MustHaveHappened();
    }


}