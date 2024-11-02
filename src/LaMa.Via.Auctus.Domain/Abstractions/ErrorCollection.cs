using ErrorOr;

namespace LaMa.Via.Auctus.Domain.Abstractions;

public sealed class ErrorCollection : List<Error>
{
    public bool HasErrors => Count > 0;

    public static ErrorCollection operator +(ErrorCollection left, ErrorCollection right)
    {
        var combinedErrors = new ErrorCollection();
        combinedErrors.AddRange(left);
        combinedErrors.AddRange(right);
        return combinedErrors;
    }

    public static ErrorCollection operator +(ErrorCollection left, Error right)
    {
        var combinedErrors = new ErrorCollection();
        combinedErrors.AddRange(left);
        combinedErrors.Add(right);
        return combinedErrors;
    }
}