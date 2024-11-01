using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public class CarBrandObjectMother
{
    public static readonly CarBrand Tesla = CarBrand.Create("Tesla", CarBrandTheme.Create(
        CssColor.Create("#CC0000"),
        CssColor.Create("#333333"),
        "Roboto, sans-serif",
        SvgImage.Create("tesla_logo.svg")
    ));

    public static readonly CarBrand Bmw = CarBrand.Create("BMW", CarBrandTheme.Create(
        CssColor.Create("#0066B1"),
        CssColor.Create("#FFFFFF"),
        "Helvetica, Arial, sans-serif",
        SvgImage.Create("bmw_logo.svg")));

    public static readonly CarBrand Audi = CarBrand.Create("Audi", CarBrandTheme.Create(
        CssColor.Create("#BB0A30"),
        CssColor.Create("#000000"),
        "AudiType, sans-serif",
        SvgImage.Create("audi_logo.svg")));

    public static readonly CarBrand Volkswagen = CarBrand.Create("Volkswagen", CarBrandTheme.Create(
        CssColor.Create("#001489"),
        CssColor.Create("#FFFFFF"),
        "VolkswagenAG, sans-serif",
        SvgImage.Create("vw_logo.svg")));

    public static readonly CarBrand Skoda =
        CarBrand.Create("Skoda", CarBrandTheme.Create(
            CssColor.Create("#007C30"),
            CssColor.Create("#FFFFFF"),
            "SkodaPro, sans-serif",
            SvgImage.Create("skoda_logo.svg")
        ));
}