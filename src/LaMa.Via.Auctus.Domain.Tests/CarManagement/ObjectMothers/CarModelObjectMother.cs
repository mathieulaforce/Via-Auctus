using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

public class CarModelObjectMother
{
    public static CarModel TeslaModel3(string? image = null)
    {
        return CarModel.Create("Tesla 3", CarBrandObjectMother.Tesla.Id,
            image != null ? SupportedImage.Create(image) : null);
    }

    public static CarModel TeslaModelY(string? image = null)
    {
        return CarModel.Create("Tesla Y", CarBrandObjectMother.Tesla.Id,
            image != null ? SupportedImage.Create(image) : null);
    }

    public static CarModel BmwX1(string? image = null)
    {
        return CarModel.Create("BMW X1", CarBrandObjectMother.Bmw.Id,
            image != null ? SupportedImage.Create(image) : null);
    }
}