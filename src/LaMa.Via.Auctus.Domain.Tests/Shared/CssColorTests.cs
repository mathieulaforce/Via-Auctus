using LaMa.Via.Auctus.Domain.Shared;
using LaMa.Via.Auctus.Domain.Shared.Errors;

namespace LaMa.Via.Auctus.Domain.Tests.Shared;

public class CssColorTests
{
    [Fact]
    public void CssColorIsHexColor()
    {
        var color = CssColor.Create("#FF0000").Value;

        color.Code.Should().Be("#FF0000");
        color.IsHexColor().Should().BeTrue();
        color.IsRgbaColor().Should().BeFalse();
        color.IsRgbColor().Should().BeFalse();
        color.IsHslColor().Should().BeFalse();
        color.IsHslaColor().Should().BeFalse();
    }

    [Fact]
    public void CssColorIsRgbColor()
    {
        var color = CssColor.Create("rgb(255, 0, 0)").Value;

        color.Code.Should().Be("rgb(255, 0, 0)");
        color.IsHexColor().Should().BeFalse();
        color.IsRgbColor().Should().BeTrue();
        color.IsRgbaColor().Should().BeFalse();
        color.IsHslColor().Should().BeFalse();
        color.IsHslaColor().Should().BeFalse();
    }

    [Fact]
    public void IsRgbaColor()
    {
        var color = CssColor.Create("rgba(255, 0, 0, 0.5)").Value;

        color.Code.Should().Be("rgba(255, 0, 0, 0.5)");
        color.IsHexColor().Should().BeFalse();
        color.IsRgbColor().Should().BeFalse();
        color.IsRgbaColor().Should().BeTrue();
        color.IsHslColor().Should().BeFalse();
        color.IsHslaColor().Should().BeFalse();
    }

    [Fact]
    public void CssColorIsHslColor()
    {
        var color = CssColor.Create("hsl(120, 100%, 50%)").Value;

        color.Code.Should().Be("hsl(120, 100%, 50%)");
        color.IsHexColor().Should().BeFalse();
        color.IsRgbaColor().Should().BeFalse();
        color.IsRgbColor().Should().BeFalse();
        color.IsHslColor().Should().BeTrue();
        color.IsHslaColor().Should().BeFalse();
    }

    [Fact]
    public void CssColorIsHslaColor()
    {
        var color = CssColor.Create("hsla(120, 100%, 50%, 0.5)").Value;

        color.Code.Should().Be("hsla(120, 100%, 50%, 0.5)");
        color.IsHexColor().Should().BeFalse();
        color.IsRgbaColor().Should().BeFalse();
        color.IsRgbColor().Should().BeFalse();
        color.IsHslColor().Should().BeFalse();
        color.IsHslaColor().Should().BeTrue();
    }

    [Fact]
    public void CreateUnsupportedColorFormatShouldReturnError()
    {
        var result = CssColor.Create("Green");
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(CssColorErrors.InvalidColorCode());
    }
}