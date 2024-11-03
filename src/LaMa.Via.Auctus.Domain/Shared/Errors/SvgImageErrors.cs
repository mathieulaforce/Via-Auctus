using ErrorOr;

namespace LaMa.Via.Auctus.Domain.Shared.Errors;

public static class SvgImageErrors
{
    public static Error InvalidExtension()
    {
        return Error.Validation(
            "SvgImage.InvalidExtension",
            "Invalid svg image extension"
        );
    }

    public static Error IsEmpty()
    {
        return Error.Validation(
            "SvgImage.IsEmpty",
            "Image cannot be empty"
        );
    }
}