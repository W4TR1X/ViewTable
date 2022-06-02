namespace w4TR1x.ViewTable.Values;

public class DateValue : CellValue<DateTime>
{
    public DateValue(DateTime value) : base(value) { }

    public override dynamic AsOrderValue()
    {
        return (TypeValue).ToBinary();
    }

    public override string ToString()
    {
        return (TypeValue).ToString("dd/MM/yyyy");
    }

    protected override void SetValue(DateTime value)
    {
        Value = value;
    }
}
