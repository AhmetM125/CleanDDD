using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();


        var context = new ValidationContext<TRequest>(request);

        var errorsDictionary = _validators
            .Select(v => v.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(failure => failure != null)
            .GroupBy(
            x => x.PropertyName,
            x => x.ErrorMessage,
            (key, value) => new
            {
                Key = key,
                Values = value.Distinct().ToArray()
            }
            )
            .ToDictionary(failure => failure.Key, failure => failure.Values);

        if (errorsDictionary.Any())
        {
            throw new ValidationException("");
        }

        return await next();
    }
    public static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErros(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
             .GetGenericTypeDefinition()
             .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
             .GetMethod(nameof(ValidationResult.WithErros))!
             .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
