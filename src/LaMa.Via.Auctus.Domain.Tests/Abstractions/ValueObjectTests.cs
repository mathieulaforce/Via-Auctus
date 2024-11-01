using LaMa.Via.Auctus.Domain.Abstractions;

namespace LaMa.Via.Auctus.Domain.Tests.Abstractions;

public class ValueObjectTests
{
    [Fact]
    public void GivenEqualValueObjectsWhenComparingThenAreEqual()
    {
        var sut1 = new SutValueObject { Value1 = "value1", Value2 = "value2" };
        var sut2 = new SutValueObject { Value1 = "value1", Value2 = "value2" };

        Assert.False(ReferenceEquals(sut1, sut2));
        sut1.Should().Be(sut2);
        sut1.Value1.Should().Be(sut2.Value1);
        sut1.Value1.Should().NotBe(sut2.Value2);
        sut1.Value2.Should().Be(sut2.Value2);
    }

    [Fact]
    public void GivenDifferentValueObjectsWhenComparingThenAreNotEqual()
    {
        var sut1 = new SutValueObject { Value1 = "value1", Value2 = "value2" };
        var sut2 = new SutValueObject { Value1 = "value1", Value2 = "VALUE2" };

        Assert.False(ReferenceEquals(sut1, sut2));
        sut1.Should().NotBe(sut2);
        sut1.Value1.Should().Be(sut2.Value1);
        sut1.Value1.Should().NotBe(sut2.Value2);
        sut1.Value2.Should().NotBe(sut2.Value2);
    }

    private class SutValueObject : ValueObject
    {
        public string Value1 { get; init; } = string.Empty;
        public string Value2 { get; init; } = string.Empty;

        protected override IEnumerable<object> GetEqualityValues()
        {
            yield return Value1;
            yield return Value2;
        }
    }
}