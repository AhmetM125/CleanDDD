namespace Domain.Shared;

public class Error
{
    public Error(string v1, string v2)
    {
        V1 = v1;
        V2 = v2;
    }

    public string V1 { get; }
    public string V2 { get; }
}
