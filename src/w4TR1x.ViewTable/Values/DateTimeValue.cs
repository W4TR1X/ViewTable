namespace w4TR1x.ViewTable.Values;

public class DateTimeValue : DateValue
{
    public DateTimeValue(DateTime value) : base(value) { }

    public override string ToString()
    {
        return ((DateTime)Value).ToString("dd/MM/yyyy HH:mm:ss");
    }
}
