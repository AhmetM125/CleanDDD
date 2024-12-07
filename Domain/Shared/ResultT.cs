namespace Domain.Shared;


public class Result<T>
{
    public Result(bool isSuccess, T error)
    {
        if (isSuccess && error != Error.None
            || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error",
                nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public T Error { get; }

    public static Result Succes() => new Result(true, Error.None);
    public static Result Failure(Error error) => new Result(false, error);
}