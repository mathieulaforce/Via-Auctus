using ErrorOr;

namespace LaMa.Via.Auctus.Domain.Shared.Errors;

public static class SupportedImageErrors
{
    public static Error InvalidExtension()
    {
        return Error.Validation(
            "SupportedImage.InvalidExtension",
            "Invalid svg image extension"
        );
    }

    public static Error IsEmpty()
    {
        return Error.Validation(
            "SupportedImage.IsEmpty",
            "Image cannot be empty"
        );
    }
}