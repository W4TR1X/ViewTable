namespace w4TR1x.ViewTable.Values;

public class StringValue : CellValue<string>
{
    public StringValue(string? value) : base(value)
    {
        Value = value?.Replace("\n", "\r\n" );
    }

    public static StringValue Empty()
    {
        return new StringValue(string.Empty);
    }

    public static StringValue[] From(params string[] values)
    {
        var valueList = new List<StringValue>();

        foreach (var value in values)
        {
            valueList.Add(new StringValue(value));
        }

        return valueList.ToArray();
    }

    protected override void SetValue(string value)
    {
        Value = value;
    }

    public override dynamic AsOrderValue()
    {
        return ToString();
    }

    public override string ToString()
    {
        return TypeValue ?? string.Empty;
    }
}