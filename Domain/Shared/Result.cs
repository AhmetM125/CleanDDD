namespace Domain.Shared;

public class Result
{
    public Result(bool v1, string v2)
    {
        V1 = v1;
        V2 = v2;
    }

    public bool V1 { get; }
    public string V2 { get; }
}
