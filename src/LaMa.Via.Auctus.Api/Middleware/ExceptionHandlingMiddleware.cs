using System.Net;
using LaMa.Via.Auctus.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LaMa.Via.Auctus.Api.Middleware;

internal class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = GetProblemDetails(exception);
            context.Response.StatusCode = problemDetails.Status.GetValueOrDefault(StatusCodes.Status500InternalServerError);
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ProblemDetails GetProblemDetails(Exception exception)
    {
        switch (exception)
        {
            case ValidationException validationException:
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "ValidationFailure",
                    Title = "Validation error",
                    Detail = "One or more validation errors has occurred"
                };
                problemDetails.Extensions["errors"] = validationException.Errors;
                return problemDetails;
            default:
                return new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "ServerError",
                    Title = "Server error",
                    Detail = "An unexpected error has occurred"
                };
        }
    }
}