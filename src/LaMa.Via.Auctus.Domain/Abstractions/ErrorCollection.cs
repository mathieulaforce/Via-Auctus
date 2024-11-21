using System.Collections;
using ErrorOr;

namespace LaMa.Via.Auctus.Domain.Abstractions;

internal interface IErrorCollection : IList<Error>
{
    bool HasErrors { get; }
}

public sealed class ErrorCollection : IErrorCollection
{
    private readonly List<Error> _errors;

    public ErrorCollection()
    {
        _errors = [];
    }

    public ErrorCollection(IEnumerable<Error> errors)
    {
        _errors = [..errors];
    }

    public bool HasErrors => Count > 0;

    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(Error item)
    {
        _errors.Add(item);
    }

    public void Clear()
    {
        _errors.Clear();
    }

    public bool Contains(Error item)
    {
        return _errors.Contains(item);
    }

    public void CopyTo(Error[] array, int arrayIndex)
    {
        _errors.CopyTo(array, arrayIndex);
    }

    public bool Remove(Error item)
    {
        return _errors.Remove(item);
    }

    public int Count => _errors.Count;
    public bool IsReadOnly => false;

    public int IndexOf(Error item)
    {
        return _errors.IndexOf(item);
    }

    public void Insert(int index, Error item)
    {
        _errors.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _errors.RemoveAt(index);
    }

    public Error this[int index]
    {
        get => _errors[index];
        set => _errors[index] = value;
    }

    private void AddRange(IEnumerable<Error> errors)
    {
        _errors.AddRange(errors);
    }


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

    // while this works, it is confusing to use.
    // public static ErrorCollection operator +(ErrorCollection left, IErrorOr errorOr)
    // {
    //     var combinedErrors = new ErrorCollection();
    //     combinedErrors.AddRange(left);
    //     if (errorOr.IsError)
    //     {
    //         combinedErrors += errorOr.Errors!;
    //     }
    //
    //     return combinedErrors;
    // }

    public static implicit operator ErrorCollection(List<Error> errors)
    {
        return new ErrorCollection(errors);
    }

    public static implicit operator List<Error>(ErrorCollection errors)
    {
        return errors._errors;
    }

    public void AddErrorsFromError(params IErrorOr?[] errors)
    {
        foreach (var errorOr in errors.Where(e => e != null))
        {
            if (errorOr is { IsError: true, Errors: not null })
            {
                _errors.AddRange(errorOr.Errors);
            }
        }
    }
}