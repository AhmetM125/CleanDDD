namespace Domain.Shared;

public class Result<TItem>
{
    private object value;
    private bool v;
    private TItem Tval;

    public Result(object value, bool v, TItem TVal)
    {
        this.value = value;
        this.v = v;
        Tval = TVal;
    }
}
