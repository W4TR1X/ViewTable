namespace w4TR1x.ViewTable.Interfaces;

public interface ICellValue
{
    public object Value { get; }

    public string? StringValue()
    {
        if (Value is string result)
        {
            return result;
        }

        return null;
    }

    public int? IntValue()
    {
        if (Value is int result)
        {
            return result;
        }

        return null;
    }

    public double? DoubleValue()
    {
        if (Value is double result)
        {
            return result;
        }

        return null;
    }

    public DateTime? DateTimeValue()
    {
        if (Value is DateTime result)
        {
            return result;
        }

        return null;
    }

    public abstract void Set(dynamic value);

    dynamic? GetValue() => IntValue() ?? DoubleValue() ?? (dynamic?)StringValue() ?? DateTimeValue() ?? null;

    abstract object AsOrderValue();
}

public interface ICellValue<out T> : ICellValue
{
    public T ResultValue { get; }

    abstract T AsTypeOrderValue();

    abstract string ToString();
}