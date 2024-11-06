using ErrorOr;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Domain.Shared;
using LaMa.Via.Auctus.Domain.Shared.Errors;

namespace LaMa.Via.Auctus.Domain.CarManagement;

public sealed record CarBrandTheme
{
    private CarBrandTheme() { }
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

    public static ErrorOr<CarBrandTheme> Create(string primaryColor, string? secondaryColor, string fontFamily,
        string svgLogo)
    {
        var errors = new ErrorCollection();
        var primary = CssColor.Create(primaryColor);
        var secondary = string.IsNullOrWhiteSpace(secondaryColor)
            ? (ErrorOr<CssColor>?)null
            : CssColor.Create(secondaryColor); 
        
        var logo = SvgImage.Create(svgLogo);

        errors.AddErrorsFromError(primary, secondary, logo);
         
        if (errors.HasErrors)
        {
            return errors.ToList();
        }

        return Create(primary.Value,secondary?.Value, fontFamily, logo.Value);
    }
}