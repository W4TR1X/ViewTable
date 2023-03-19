namespace w4TR1x.ViewTable.Values;

public class IntValue : CellValue<int>
{
    public IntValue(int value) : base(value)
    {    }

    protected override void SetValue(int value)
    {
        Value = value;
    }

    public static IntValue From(int value)
    {
        return new IntValue(value);
    }


    public override dynamic? AsOrderValue()
    {
        return Value;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }
}
