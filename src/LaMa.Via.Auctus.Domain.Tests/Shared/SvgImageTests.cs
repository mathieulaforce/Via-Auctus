using LaMa.Via.Auctus.Domain.Shared;
using LaMa.Via.Auctus.Domain.Shared.Errors;

namespace LaMa.Via.Auctus.Domain.Tests.Shared;

public class SvgImageTests
{
    [Fact]
    public void SvgImageShouldReturnCorrectSvgImage()
    {
        var svg = SvgImage.Create("img.svg");
        svg.Value.Url.Should().Be("img.svg");
    }

    [Fact]
    public void PngImageShouldReturnError()
    {
        var result = SvgImage.Create("img.png");
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(SvgImageErrors.InvalidExtension().Code);
    }

    [Fact]
    public void EmptyStringShouldReturnError()
    {
        var result = SvgImage.Create("");
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(SvgImageErrors.IsEmpty().Code);
    }
}