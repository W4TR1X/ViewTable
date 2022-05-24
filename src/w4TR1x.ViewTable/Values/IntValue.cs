namespace w4TR1x.ViewTable.Values;

[Serializable]
public class IntValue : CellValue<int>
{
    public IntValue(int resultValue) : base(resultValue)
    {
    }

    protected override void SetValue(int value)
    {
        Value = value;
    }

    public static IntValue From(int value)
    {
        return new IntValue(value);
    }


    public override object AsOrderValue()
    {
        return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
