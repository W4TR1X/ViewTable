namespace w4TR1x.ViewTable.Values;

[Serializable]
public class DateValue : CellValue<DateTime>
{
    public DateValue(DateTime resultValue) : base(resultValue)
    {
    }

    public override dynamic AsOrderValue()
    {
        return (ResultValue).ToBinary();
    }

    public override string ToString()
    {
        return (ResultValue).ToString("dd/MM/yyyy");
    }

    protected override void SetValue(DateTime value)
    {
        Value = value;
    }
}
