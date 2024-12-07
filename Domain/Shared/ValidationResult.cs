using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Shared;

public class ValidationResult : Result, IValidationResult
{
    public ValidationResult(Error[] errors)
        : base(false, "Validation error")
        => Errors = errors;

    public Error[] Errors { get; }
    public static ValidationResult WithErros(Error[] errors)
        => new(errors);
}
