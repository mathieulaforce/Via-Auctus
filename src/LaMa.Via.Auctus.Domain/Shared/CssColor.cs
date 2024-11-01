using System.Text.RegularExpressions;

namespace LaMa.Via.Auctus.Domain.Shared;

public sealed record CssColor
{
    private static readonly Regex HexColorRegex = new(
        @"^#([0-9a-fA-F]{3}|[0-9a-fA-F]{6})$",
        RegexOptions.Compiled
    );

    // RGB Color: rgb(255, 0, 0)
    private static readonly Regex RgbColorRegex = new(
        @"^rgb\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*\)$",
        RegexOptions.Compiled
    );

    // RGBA Color: rgba(255, 0, 0, 0.5)
    private static readonly Regex RgbaColorRegex = new(
        @"^rgba\(\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(\d{1,3})\s*,\s*(0|1|0?\.\d+)\s*\)$",
        RegexOptions.Compiled
    );

    // HSL Color: hsl(120, 100%, 50%)
    private static readonly Regex HslColorRegex = new(
        @"^hsl\(\s*(\d{1,3})\s*,\s*([0-9]{1,3})%\s*,\s*([0-9]{1,3})%\s*\)$",
        RegexOptions.Compiled
    );

    // HSLA Color: hsla(120, 100%, 50%, 0.5)
    private static readonly Regex HslaColorRegex = new(
        @"^hsla\(\s*(\d{1,3})\s*,\s*([0-9]{1,3})%\s*,\s*([0-9]{1,3})%\s*,\s*(0|1|0?\.\d+)\s*\)$",
        RegexOptions.Compiled
    );

    private CssColor(string code)
    {
        Code = code;
    }

    public string Code { get; }

    public static bool CanCreate(string code)
    {
        return IsHexColor(code) || IsRgbColor(code) || IsRgbaColor(code) || IsHslColor(code) || IsHslaColor(code);
    }

    public static CssColor Create(string code)
    {
        if (CanCreate(code))
        {
            return new CssColor(code);
        }

        throw new FormatException($"Invalid css color code: '{code}'. Supported types are hex, rgb(a) and hsl(a)");
    }

    public static bool IsHexColor(string colorCode)
    {
        return HexColorRegex.IsMatch(colorCode);
    }

    public static bool IsRgbColor(string colorCode)
    {
        return RgbColorRegex.IsMatch(colorCode);
    }

    public static bool IsRgbaColor(string colorCode)
    {
        return RgbaColorRegex.IsMatch(colorCode);
    }

    public static bool IsHslColor(string colorCode)
    {
        return HslColorRegex.IsMatch(colorCode);
    }

    public static bool IsHslaColor(string colorCode)
    {
        return HslaColorRegex.IsMatch(colorCode);
    }

    public bool IsRgbColor()
    {
        return IsRgbColor(Code);
    }

    public bool IsHexColor()
    {
        return IsHexColor(Code);
    }

    public bool IsRgbaColor()
    {
        return IsRgbaColor(Code);
    }

    public bool IsHslColor()
    {
        return IsHslColor(Code);
    }

    public bool IsHslaColor()
    {
        return IsHslaColor(Code);
    }
}