﻿using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public static class CarBrandObjectMother
{
    public static CarBrand Tesla =
        CarBrand.Create("Tesla", CarBrandTheme.Create(
            CssColor.Create("#CC0000").Value,
            CssColor.Create("#333333").Value,
            "Roboto, sans-serif",
            SvgImage.Create("tesla_logo.svg").Value
        ));

    public static CarBrand Bmw =
        CarBrand.Create("BMW", CarBrandTheme.Create(
            CssColor.Create("#0066B1").Value,
            CssColor.Create("#FFFFFF").Value,
            "Helvetica, Arial, sans-serif",
            SvgImage.Create("bmw_logo.svg").Value));

    public static CarBrand Audi =
        CarBrand.Create("Audi", CarBrandTheme.Create(
            CssColor.Create("#BB0A30").Value,
            CssColor.Create("#000000").Value,
            "AudiType, sans-serif",
            SvgImage.Create("audi_logo.svg").Value));

    public static CarBrand Volkswagen =
        CarBrand.Create("Volkswagen", CarBrandTheme.Create(
            CssColor.Create("#001489").Value,
            CssColor.Create("#FFFFFF").Value,
            "VolkswagenAG, sans-serif",
            SvgImage.Create("vw_logo.svg").Value));


    public static CarBrand Skoda = CarBrand.Create("Skoda", CarBrandTheme.Create(
        CssColor.Create("#007C30").Value,
        CssColor.Create("#FFFFFF").Value,
        "SkodaPro, sans-serif",
        SvgImage.Create("skoda_logo.svg").Value
    ));

    public static CarBrand CreateNewFrom(CarBrand carBrand)
    {
        return CarBrand.Create(carBrand.Name, carBrand.Theme);
    }
}