namespace w4TR1x.ViewTable.Values;

[Serializable]
[JsonConverter(typeof(CellValueConverter))]
public abstract class CellValue<T> : CellValue
{
    [JsonIgnore]
    public T? TypeValue { get; private set; }

    public override dynamic? Value
    {
        get => TypeValue;
        protected set => TypeValue = value;
    }

    [JsonIgnore]
    public Type ValueType => typeof(T);

    [JsonConstructor]
    protected CellValue(T? value)
    {
        Value = value;
        TypeValue = value;
    }

    public T? AsTypeOrderValue() => TypeValue;

    public void Set(dynamic value)
    {
        var result = Convert.ChangeType(value, ValueType);

        if (result == null)
        {
            throw new ArgumentOutOfRangeException($"{nameof(value)} is {value.GetType()}, but it must be {ValueType}");
        }

        SetValue((T)result);
    }

    protected abstract void SetValue(T value);
    public override dynamic? GetValue() => TypeValue;
}

[Serializable]
[JsonConverter(typeof(CellValueConverter))]
public abstract class CellValue
{
    //public abstract string Type { get; }

    public virtual dynamic? Value { get; protected set; }

    public virtual dynamic? GetValue() => Value;
    public abstract dynamic? AsOrderValue();
}