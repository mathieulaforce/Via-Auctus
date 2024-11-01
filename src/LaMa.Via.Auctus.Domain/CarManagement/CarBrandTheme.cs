using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarBrandTheme
{
    private CarBrandTheme(CssColor primaryColor, CssColor? secondaryColor, string fontFamily, SvgImage logo)
    {
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        FontFamily = fontFamily;
        Logo = logo;
    }

    public CssColor PrimaryColor { get; private set; }
    public CssColor? SecondaryColor { get; private set; }
    public string FontFamily { get; private set; }
    public SvgImage Logo { get; private set; }

    public static CarBrandTheme Create(CssColor primaryColor, CssColor? secondaryColor, string fontFamily,
        SvgImage logo)
    {
        return new CarBrandTheme(primaryColor, secondaryColor, fontFamily, logo);
    }
}