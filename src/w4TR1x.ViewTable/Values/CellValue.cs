namespace w4TR1x.ViewTable.Values;

[Serializable]
public abstract class CellValue<T> : ICellValue<T>
{
    public T ResultValue => (T)Value;

    private bool _disposed = false;

    public dynamic Value { get; protected set; }

    public Type ValueType { get; private set; }

    [JsonConstructor]
    protected CellValue(dynamic value, Type valueType)
    {
        Value = value;
        ValueType = valueType;
    }

    protected CellValue(T resultValue!!)
    {
        ValueType = resultValue.GetType();
        Value = resultValue;
    }

    public abstract dynamic AsOrderValue();

    public T AsTypeOrderValue() => ResultValue;

    public void Set(dynamic value)
    {
        var result = Convert.ChangeType(value, ValueType);

        if (result == null)
        {
            throw new ArgumentOutOfRangeException($"{nameof(value)} is {value.GetType()}, but it must be {Value.GetType()}");
        }

        SetValue((T)result);
    }

    protected abstract void SetValue(T value);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
    }
}