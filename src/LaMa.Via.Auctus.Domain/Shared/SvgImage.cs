namespace LaMa.Via.Auctus.Domain.Shared;

public sealed record SvgImage
{
    private SvgImage(string url)
    {
        Url = url;
    }

    public string Url { get; private set; }

    public static SvgImage Create(string url)
    {
        // TODO result pattern
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        var extension = Path.GetExtension(url).ToLowerInvariant();
        if (extension != ".svg")
        {
            throw new ApplicationException($"Invalid svg image extension: '{extension}'");
        }

        return new SvgImage(url);
    }
}