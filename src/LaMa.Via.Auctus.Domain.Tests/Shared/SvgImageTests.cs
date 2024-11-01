using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.Shared;

public class SvgImageTests
{
    [Fact]
    public void SvgImageShouldReturnCorrectSvgImage()
    {
        var svg = SvgImage.Create("img.svg");
        svg.Url.Should().Be("img.svg");
    }
    
    [Fact]
    public void PngImageShouldThrowException()
    {
        var action = () => SvgImage.Create("img.png");
        action.Should().Throw<ApplicationException>();
    }
    
    [Fact]
    public void EmptyStringShouldThrowException()
    {
        var action = () => SvgImage.Create("");
        action.Should().Throw<ArgumentException>();
    }
}