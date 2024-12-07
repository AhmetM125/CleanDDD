
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using IExceptionHandler = Microsoft.AspNetCore.Diagnostics.IExceptionHandler;

namespace WebApi.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        context.Response.StatusCode = e switch
        {
            BadHttpRequestException or ValidationException => (int)HttpStatusCode.BadRequest,
            WebinarNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
}

public class ExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

  

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Status = httpContext.Response.StatusCode,
            Detail = exception.StackTrace
        };

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails);

        return true;

    }
}

public class ExceptionHandlerMiddlewareV2 : System.Web.Http.ExceptionHandling.IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddlewareV2(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        var problemDetails = new ProblemDetails
        {
            Title = context.Exception.Message,
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = context.Exception.StackTrace
        };

        context.Result = new ResponseMessageResult(context.Request.CreateResponse((HttpStatusCode)problemDetails.Status, problemDetails));

        return Task.CompletedTask;
    }

    
}
