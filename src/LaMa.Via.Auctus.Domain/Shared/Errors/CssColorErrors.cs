using ErrorOr;

namespace LaMa.Via.Auctus.Domain.Shared.Errors;

public static class CssColorErrors
{
    public static Error InvalidColorCode()
    {
        return Error.Validation(
            "CssColor.InvalidColorCode",
            $"Invalid css color code. Supported types are hex, rgb(a) and hsl(a)"
        );
    }
}