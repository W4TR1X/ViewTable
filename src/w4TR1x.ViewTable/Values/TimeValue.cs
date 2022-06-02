namespace w4TR1x.ViewTable.Values;

public class TimeValue : CellValue<TimeSpan>
{
    public TimeValue(TimeSpan value) : base(value) { }

    public override dynamic AsOrderValue()
    {
        return DateTime.FromOADate(TypeValue.TotalMilliseconds).ToBinary();
    }

    public override string ToString()
    {
        return (TypeValue).ToString("hh\\:mm\\:ss");
    }

    protected override void SetValue(TimeSpan value)
    {
        Value = value;
    }
}
