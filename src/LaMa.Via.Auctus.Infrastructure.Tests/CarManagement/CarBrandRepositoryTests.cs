using FakeItEasy;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;
using LaMa.Via.Auctus.Infrastructure.CarManagement; 
using MediatR; 

namespace LaMa.Via_Auctus.Infrastructure.Tests.CarManagement;

public class CarBrandRepositoryTests
{
    private IPublisher _publisher;
    private ICarBrandWriteRepository _sut;
    
    public CarBrandRepositoryTests()
    {
        _publisher = A.Fake<IPublisher>();
        var context = ApplicationContextTestFactory.CreateWriteContext(_publisher);
        _sut =new CarBrandWriteRepository(context);
    }
    [Fact]
    public async Task BasicAddAndReadTest()
    {
        var brand = CarBrandObjectMother.Audi;
        await _sut.Add(brand, default);
        var result = await _sut.Get(brand.Id,default);
        result.Should().Be(brand);
    }
    
    [Fact]
    public async Task EmptyDatabaseTest()
    {
        var brand = CarBrandObjectMother.Audi; 
        var result = await _sut.Get(brand.Id,default);
        result.Should().BeNull();
    }
}