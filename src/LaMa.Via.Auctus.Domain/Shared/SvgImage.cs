using ErrorOr;
using LaMa.Via.Auctus.Domain.Shared.Errors;

namespace LaMa.Via.Auctus.Domain.Shared;

public sealed record SvgImage
{
    private SvgImage(string url)
    {
        Url = url;
    }

    public string Url { get; private set; }

    public static ErrorOr<SvgImage> Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return SvgImageErrors.IsEmpty();
        }

        var extension = Path.GetExtension(url).ToLowerInvariant();
        if (extension != ".svg")
        {
            return SvgImageErrors.InvalidExtension();
        }

        return new SvgImage(url);
    }
}