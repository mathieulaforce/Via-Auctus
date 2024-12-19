using FakeItEasy;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Write;
using MediatR;

namespace LaMa.Via_Auctus.Infrastructure.Tests.Interceptors;

public class DomainInterceptorTests
{
    [Fact]
    public async Task GivenEntityWhenSaveChangesAsyncThenPublishesDomainEvents()
    {
        var publisher = A.Fake<IPublisher>();
        var context = ApplicationContextTestFactory.CreateWriteContext(publisher);
        var brandRepo = new CarBrandWriteRepository(context);
        var brand = CarBrandObjectMother.CreateNewFrom(CarBrandObjectMother.Audi);
        IUnitOfWork uow = context;

        await brandRepo.Add(brand);
        await uow.SaveChangesAsync();

        A.CallTo(() => publisher.Publish(A<IDomainEvent>._, default)).MustHaveHappened();
    }

    [Fact]
    public async Task GivenEntityWhenSaveChangesThenPublishesDomainEvents()
    {
        var publisher = A.Fake<IPublisher>();
        var context = ApplicationContextTestFactory.CreateWriteContext(publisher);
        var brandRepo = new CarBrandWriteRepository(context);
        var brand = CarBrandObjectMother.CreateNewFrom(CarBrandObjectMother.Audi);

        await brandRepo.Add(brand);
        context.SaveChanges(default);

        A.CallTo(() => publisher.Publish(A<IDomainEvent>._, default)).MustHaveHappened();
    }
}