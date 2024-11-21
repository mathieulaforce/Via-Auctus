using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.Tests.Abstractions;

public class AggregateRootTests
{
    [Fact]
    public void ComparingAggregateRootWithSameIdAreEqual()
    {
        var guid = Guid.NewGuid();

        var sut1 = new SutAggregateRoot(new SutAggregateRootId(guid), "sut1");
        var sut2 = new SutAggregateRoot(new SutAggregateRootId(guid), "sut2");

        Assert.False(ReferenceEquals(sut1, sut2));
        sut1.Should().Be(sut2);
        sut1.Name.Should().NotBe(sut2.Name);
        sut1.Id.Should().Be(sut2.Id);
    }

    [Fact]
    public void GivenAggregateRootWithDifferentIdsThenAreNotEqual()
    {
        var sut1 = new SutAggregateRoot(new SutAggregateRootId(Guid.NewGuid()), "sut");
        var sut2 = new SutAggregateRoot(new SutAggregateRootId(Guid.NewGuid()), "sut");

        Assert.False(ReferenceEquals(sut1, sut2));
        sut1.Should().NotBe(sut2);
        sut1.Name.Should().Be(sut2.Name);
        sut1.Id.Should().NotBe(sut2.Id);
    }

    private record SutAggregateRootId(Guid value)
    {
    }

    private class SutAggregateRoot(SutAggregateRootId id, string name) : AggregateRoot<SutAggregateRootId>(id)
    {
        public string Name { get; } = name;
    }
}