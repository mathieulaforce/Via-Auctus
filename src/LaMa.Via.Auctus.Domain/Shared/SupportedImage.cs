using ErrorOr;
using LaMa.Via.Auctus.Domain.Shared.Errors;

namespace LaMa.Via.Auctus.Domain.Shared;

public sealed record SupportedImage
{
    private static readonly IReadOnlyCollection<string> SupportedExtensions =
        [".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg"];

    private SupportedImage(string url)
    {
        Url = url;
    }

    public string Url { get; private set; }

    public static ErrorOr<SupportedImage> Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return SupportedImageErrors.IsEmpty();
        }

        var extension = Path.GetExtension(url).ToLowerInvariant();
        if (!SupportedExtensions.Contains(extension))
        {
            return SupportedImageErrors.InvalidExtension();
        }

        return new SupportedImage(url);
    }
}