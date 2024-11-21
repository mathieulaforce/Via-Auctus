using LaMa.Via.Auctus.Domain.Shared;
using LaMa.Via.Auctus.Domain.Shared.Errors;

namespace LaMa.Via.Auctus.Domain.Tests.Shared;

public class SupportedImageTests
{
    [Theory]
    [InlineData(".jpg")]
    [InlineData(".jpeg")]
    [InlineData(".png")]
    [InlineData(".gif")]
    [InlineData(".webp")]
    [InlineData(".svg")]
    public void SvgImageShouldReturnCorrectImage(string extension)
    {
        var svg = SupportedImage.Create($"img{extension}").Value;
        svg.Url.Should().Be($"img{extension}");
    }

    [Fact]
    public void EmptyStringShouldReturnError()
    {
        var result = SupportedImage.Create("");
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(SupportedImageErrors.IsEmpty().Code);
    }

    [Fact]
    public void UnsupportedTypeShouldReturnError()
    {
        var result = SupportedImage.Create("un.supported");
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be(SupportedImageErrors.InvalidExtension().Code);
    }
}