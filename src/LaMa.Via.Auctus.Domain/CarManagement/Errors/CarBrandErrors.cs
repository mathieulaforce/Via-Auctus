using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public class CarBrandErrors
{
    public static Error BrandNotFound(CarBrandId brandId)
    {
        return Error.Validation(
            "CarBrand.NotFound",
            "Car brand not found",
            new Dictionary<string, object>
            {
                { "BrandId", brandId }
            }
        );
    }
    
    public static Error BrandNotFound(string name)
    {
        return Error.Validation(
            "CarBrand.NotFound",
            "Car brand not found",
            new Dictionary<string, object>
            {
                { "Name", name }
            }
        );
    }
    
    public static Error BrandAlreadyExists(CarBrandId brandId , string name)
    {
        return Error.Validation(
            "CarBrand.AlreadyExists",
            "Car brand already exists",
            new Dictionary<string, object>
            {
                { "BrandId", brandId },
                { "Name", name }
            }
        );
    }
}