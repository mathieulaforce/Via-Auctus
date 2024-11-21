using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public class GeneralErrors
{
    public static Error NotFound(string objectName, object id)
    {
        return Error.Validation(
            "General.NotFound",
            "Object not found",
            new Dictionary<string, object>
            {
                { "Object", objectName },
                { "Id", id }
            }
        );
    }
}