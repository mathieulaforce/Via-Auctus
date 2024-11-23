using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Application.CarManagement.CarModels;
using LaMa.Via.Auctus.Application.CarManagement.CarModelVersions;
using LaMa.Via.Auctus.Application.CarManagement.Cars;
using LaMa.Via.Auctus.Application.CarManagement.Cars.Create;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Application.Tests.CarManagement.Cars.Create;

public class CreateCarCommandHandlerTests
{
    private readonly ICarBrandWriteRepository _carBrandWriteRepository;
    private readonly ICarModelVersionWriteRepository _carModelVersionWriteRepository;
    private readonly ICarModelWriteRepository _carModelWriteRepository;
    private readonly ICarWriteRepository _carWriteRepository;
    private readonly ICommandHandler<CreateCarCommand, CarId> _sut;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCarCommandHandlerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _carWriteRepository = A.Fake<ICarWriteRepository>();
        _carBrandWriteRepository = A.Fake<ICarBrandWriteRepository>();
        _carModelWriteRepository = A.Fake<ICarModelWriteRepository>();
        _carModelVersionWriteRepository = A.Fake<ICarModelVersionWriteRepository>();
        _sut = new CreateCarCommandHandler(_carWriteRepository, _carBrandWriteRepository, _carModelWriteRepository,
            _carModelVersionWriteRepository, _unitOfWork);
    }

    [Fact]
    public async Task GivenCarWithoutRegistrationWhenHandleThenCreateCar()
    { 
        var brand = CarBrandObjectMother.Tesla;
        var model = CarModelObjectMother.TeslaModelY(); 
        var engine = EngineObjectMother.TeslaModel3Motor;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(model.Id, "Long Range All-Wheel Drive", 2024, engines);

        A.CallTo(() => _carBrandWriteRepository.Get(brand.Id, default)).Returns(brand);
        A.CallTo(() => _carModelWriteRepository.Get(model.Id, default)).Returns(model);
        A.CallTo(() => _carModelVersionWriteRepository.Get(version.Id, default)).Returns(version);
        var capturedCar = A.Captured<Car>();
        A.CallTo(() => _carWriteRepository.Add(capturedCar._, default)).DoesNothing();

        var command = new CreateCarCommand(brand.Id, model.Id, version.Id, engine.Id, null);
        var result = await _sut.Handle(command, default); 
        
        result.IsError.Should().BeFalse();
        var carToAssert = capturedCar.GetLastValue();
        carToAssert.BrandId.Should().Be(brand.Id);
        carToAssert.ModelId.Should().Be(model.Id);
        carToAssert.VersionId.Should().Be(version.Id);
        carToAssert.Registration.Should().BeNull();
        
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustHaveHappened();
    }
    
    [Fact]
    public async Task GivenCarWithRegistrationWhenHandleThenCreateCar()
    { 
        var brand = CarBrandObjectMother.Tesla;
        var model = CarModelObjectMother.TeslaModelY(); 
        var engine = EngineObjectMother.TeslaModel3Motor;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(model.Id, "Long Range All-Wheel Drive", 2024, engines);
        var registration = new CarRegistrationInformation("Sample License Plate", DateOnly.MinValue, DateOnly.MaxValue);
        
        A.CallTo(() => _carBrandWriteRepository.Get(brand.Id, default)).Returns(brand);
        A.CallTo(() => _carModelWriteRepository.Get(model.Id, default)).Returns(model);
        A.CallTo(() => _carModelVersionWriteRepository.Get(version.Id, default)).Returns(version);
        var capturedCar = A.Captured<Car>();
        A.CallTo(() => _carWriteRepository.Add(capturedCar._, default)).DoesNothing();

        var command = new CreateCarCommand(brand.Id, model.Id, version.Id, engine.Id, registration);
        var result = await _sut.Handle(command, default); 
        
        result.IsError.Should().BeFalse();
        var carToAssert = capturedCar.GetLastValue();
        carToAssert.BrandId.Should().Be(brand.Id);
        carToAssert.ModelId.Should().Be(model.Id);
        carToAssert.VersionId.Should().Be(version.Id);
        carToAssert.Registration.Should().Be(CarRegistration.Create(registration.LicencePlate,registration.FirstRegistrationDate,registration.RegistrationExpiryDate).Value);
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustHaveHappened();
    }
    
    [Fact]
    public async Task GivenInvalidCarWhenHandleThenReturnsErrors()
    { 
        var brand = CarBrandObjectMother.Tesla;
        var model = CarModelObjectMother.TeslaModelY(); 
        var engine = EngineObjectMother.TeslaModel3Motor;
        var engines = new Engines();
        engines.AddEngine(engine);
        var version = CarModelVersion.Create(model.Id, "Long Range All-Wheel Drive", 2024, engines);
        var registration = new CarRegistrationInformation("Sample License Plate", DateOnly.MaxValue, DateOnly.MinValue);
         
        var command = new CreateCarCommand(brand.Id, model.Id, version.Id, engine.Id, registration);
        var result = await _sut.Handle(command, default); 
        
        result.IsError.Should().BeTrue();
        result.Errors.Count.Should().Be(4);
        
        A.CallTo(() => _unitOfWork.SaveChangesAsync(default)).MustNotHaveHappened();
    }
}