using ErrorOr;

namespace LaMa.Via.Auctus.Domain.CarManagement.Errors;

public static class CarErrors
{
    public static Error CarAlreadyRegistered(CarId carId)
    {
        return Error.Validation(
            "Car.AlreadyRegistered",
            "The car is already registered",
            new Dictionary<string, object>
            {
                { "Id", carId }
            }
        );
    }

    public static Error CarBrandDoesNotSupportModel(CarBrandId brandId, CarModelId modelId)
    {
        return Error.Validation(
            "Car.UnsupportedBrandModel",
            "The car brand does not support this model.",
            new Dictionary<string, object>
            {
                { "BrandId", brandId },
                { "ModelId", modelId }
            }
        );
    }

    public static Error CarModelDoesNotSupportVersion(CarBrandId brandId, CarModelId modelId,
        CarModelVersionId versionId)
    {
        return Error.Validation(
            "Car.UnsupportedModelVersion",
            "The car model does not support this version.",
            new Dictionary<string, object>
            {
                { "BrandId", brandId },
                { "ModelId", modelId },
                { "VersionId", versionId }
            }
        );
    }

    public static Error CarVersionDoesNotSupportEngine(CarBrandId brandId, CarModelId modelId,
        CarModelVersionId versionId, EngineId engineId)
    {
        return Error.Validation(
            "Car.UnsupportedEngine",
            "The car version does not support this engine",
            new Dictionary<string, object>
            {
                { "BrandId", brandId },
                { "ModelId", modelId },
                { "VersionId", versionId },
                { "EngineId", engineId }
            }
        );
    }
}