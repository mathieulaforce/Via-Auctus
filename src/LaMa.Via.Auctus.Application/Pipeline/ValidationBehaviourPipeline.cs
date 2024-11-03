using FluentValidation;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.Exceptions;
using MediatR;

namespace LaMa.Via.Auctus.Application.Pipeline;

public class ValidationBehaviourPipeline<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviourPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);
        
        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}