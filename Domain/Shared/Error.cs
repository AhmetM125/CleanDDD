namespace Domain.Shared;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new Error("None");

    public static implicit operator Result(Error error)
        => new Result(false, error);
}


