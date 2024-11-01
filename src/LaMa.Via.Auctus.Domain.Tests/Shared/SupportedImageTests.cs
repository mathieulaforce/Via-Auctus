using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.Shared;

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
        var svg = SupportedImage.Create($"img{extension}");
        svg.Url.Should().Be($"img{extension}");
    }
     
    [Fact]
    public void EmptyStringShouldThrowException()
    {
        var action = () => SupportedImage.Create("");
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void UnsupportedTypeShouldThrowException()
    {
        var action = () => SupportedImage.Create("un.supported");
        action.Should().Throw<ApplicationException>();
    }
}