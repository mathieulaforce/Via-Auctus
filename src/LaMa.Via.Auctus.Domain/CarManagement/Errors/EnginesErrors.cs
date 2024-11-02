using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public static class EnginesErrors
{
    public static Error EngineAlreadyRegistered(EngineId engineId, string name)
    {
        return Error.Validation(
            "Engines.EngineAlreadyRegistered",
            "Engine already registered.",
            new Dictionary<string, object>
            {
                { "name", name },
                { "engineId", engineId }
            }
        );
    }
}