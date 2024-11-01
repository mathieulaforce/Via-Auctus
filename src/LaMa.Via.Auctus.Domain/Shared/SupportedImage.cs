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

    public static SupportedImage Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        var extension = Path.GetExtension(url).ToLowerInvariant();
        if (!SupportedExtensions.Contains(extension))
        {
            throw new ApplicationException($"Invalid image extension: '{extension}'");
        }

        return new SupportedImage(url);
    }
}