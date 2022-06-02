using w4TR1x.ViewTable.Values;

namespace w4TR1x.ViewTable.Models;

[Serializable]
public class CellPageValue
{
    public CellValue Value { get; set; }
    public CellStyle? Style { get; set; }

    public string? PopupTitle { get; set; }
    public string? PopupText { get; set; }

    public CellPageValue(CellValue value, CellStyle? style)
    {
        Value = value;
        Style = style;
    }

    public dynamic? AsOrderValue()
    {
        return Value?.AsOrderValue();
    }
}