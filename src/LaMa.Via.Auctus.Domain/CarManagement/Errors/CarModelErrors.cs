using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public class CarModelErrors
{
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