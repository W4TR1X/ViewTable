﻿namespace w4TR1x.ViewTable.Models;

[Serializable]
public class CellPageValue : ICellPageValue
{
    public ICellValue Value { get; set; }
    public ICellStyle? Style { get; set; }

    public string? PopupTitle { get; set; }
    public string? PopupText { get; set; }

    public CellPageValue(ICellValue value, ICellStyle? style)
    {
        Value = value;
        Style = style;
    }

    public dynamic AsOrderValue()
    {
        return Value.AsOrderValue();
    }
}