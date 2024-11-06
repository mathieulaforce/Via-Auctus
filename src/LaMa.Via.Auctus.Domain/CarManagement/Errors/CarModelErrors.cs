using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public class CarModelErrors
{
    public static Error ModelNotFound(CarModelId modelId)
    {
        return Error.Validation(
            "CarModel.NotFound",
            "Car model not found",
            new Dictionary<string, object>
            {
                { "ModelId", modelId }
            }
        );
    }

    public static Error ModelNotFound(string name)
    {
        return Error.Validation(
            "CarModel.NotFound",
            "Car model not found",
            new Dictionary<string, object>
            {
                { "Name", name }
            }
        );
    }

    public static Error ModelAlreadyExists(CarModelId modelId, string name)
    {
        return Error.Validation(
            "CarModel.AlreadyExists",
            "Car model already exists",
            new Dictionary<string, object>
            {
                { "ModelId", modelId },
                { "Name", name }
            }
        );
    }
}