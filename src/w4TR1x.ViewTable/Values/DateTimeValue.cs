namespace w4TR1x.ViewTable.Values;

[Serializable]
public class DateTimeValue : DateValue
{
    public DateTimeValue(DateTime resultValue) : base(resultValue)
    {
    }

    public override string ToString()
    {
        return ((DateTime)Value).ToString("dd/MM/yyyy HH:mm:ss");
    }
}
