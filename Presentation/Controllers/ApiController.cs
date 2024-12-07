using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    public ApiController(ISender sender)
    {
        Sender = sender;
    }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
            BadRequest(CreateProblemDetails(
                "Validation Error",StatusCodes.Status400BadRequest,
                result.Error,
                validationResult.Errors)),

                _ => BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };
    }

    protected static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null)

        => new()
        {
            Title = title,
            Status = status,
            Detail = error.Message,
            Type = error.Type,
            Instance = error.Instance,
            Extensions = errors?.ToDictionary(x => x.Key, x => x.Value)
        };
}
