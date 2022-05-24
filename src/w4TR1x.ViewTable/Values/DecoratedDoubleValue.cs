namespace w4TR1x.ViewTable.Values;

[Serializable]
public class DecoratedDoubleValue : DoubleValue
{
    public readonly string? BeforeText;
    public readonly string? AfterText;

    public DecoratedDoubleValue(double resultValue, string? beforeText = null, string? afterText = null,
        ValueEnum decimalCount = ValueEnum.Int, ValueEnum zeroDecimalCount = ValueEnum.Triple,
        bool displayThousandSeparator = false, bool alwaysDisplayDecimals = false)
        : base(resultValue, decimalCount, zeroDecimalCount, displayThousandSeparator, alwaysDisplayDecimals)
    {
        BeforeText = beforeText;
        AfterText = afterText;
    }

    public override string ToString()
    {
        var value = base.ToString();
        if (value.Length == 0 || (double)Value == 0) return value;

        if (BeforeText != null)
        {
            value = $"{BeforeText} {value}";
        }

        if (AfterText != null)
        {
            value = $"{value} {AfterText}";
        }

        return value;
    }
}
