using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.Tests.Abstractions;

public class EntityTests
{
    [Fact]
    public void GivenEqualEntityIdsWhenComparingThenAreEqual()
    {
        var guid = Guid.NewGuid();
        var sut1 = new SutEntity(guid, "sut1");
        var sut2 = new SutEntity(guid, "sut2");

        Assert.False(ReferenceEquals(sut1, sut2));
        sut1.Should().Be(sut2);
        sut1.Value.Should().NotBe(sut2.Value);
    }

    [Fact]
    public void GivenDifferentEntityIdsWhenComparingThenAreNotEqual()
    {
        var sut1 = new SutEntity(Guid.NewGuid(), "sut");
        var sut2 = new SutEntity(Guid.NewGuid(), "sut");

        Assert.False(ReferenceEquals(sut1, sut2));
        sut1.Should().NotBe(sut2);
        sut1.Value.Should().Be(sut2.Value);
    }

    private class SutEntity(Guid id, string value) : Entity<Guid>(id)
    {
        public string Value { get; } = value;
    }
}