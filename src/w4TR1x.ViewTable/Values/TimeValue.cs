namespace w4TR1x.ViewTable.Values;

[Serializable]
public class TimeValue : CellValue<TimeSpan>
{
    public TimeValue(TimeSpan resultValue) : base(resultValue)
    {
    }

    public override dynamic AsOrderValue()
    {
        return DateTime.FromOADate(ResultValue.TotalMilliseconds).ToBinary();
    }

    public override string ToString()
    {
        return (ResultValue).ToString("hh\\:mm\\:ss");
    }

    protected override void SetValue(TimeSpan value)
    {
        Value = value;
    }
}
