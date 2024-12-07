namespace Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new Error("Validation error", "");
    Error[] Errors { get; }

}
