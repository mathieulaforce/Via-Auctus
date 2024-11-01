using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.Shared;

public class CssColorTests
{
    [Fact]
    public void CssColorIsHexColor()
    {
        var color = CssColor.Create("#FF0000");

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
        var color = CssColor.Create("rgb(255, 0, 0)");

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
        var color = CssColor.Create("rgba(255, 0, 0, 0.5)");

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
        var color = CssColor.Create("hsl(120, 100%, 50%)");

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
        var color = CssColor.Create("hsla(120, 100%, 50%, 0.5)");

        color.Code.Should().Be("hsla(120, 100%, 50%, 0.5)");
        color.IsHexColor().Should().BeFalse();
        color.IsRgbaColor().Should().BeFalse();
        color.IsRgbColor().Should().BeFalse();
        color.IsHslColor().Should().BeFalse();
        color.IsHslaColor().Should().BeTrue();
    }
    
    [Fact]
    public void CreateUnsupportedColorFormatThenThrowsException()
    {
        var action = () => CssColor.Create("Green");
        action.Should().Throw<FormatException>();
    }
}