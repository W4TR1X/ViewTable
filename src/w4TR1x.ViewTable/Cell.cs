using w4TR1x.ViewTable.Values;

namespace w4TR1x.ViewTable;

[Serializable]
public class Cell //: ICell
{
    [JsonIgnore]
    public Row? Parent { get; set; } = null;

    public CellStyle BaseStyle { get; set; }

    public List<CellPageValue> Values { get; } = new();

    public string Identifier { get; }
    public bool Hidden { get; set; }
    public string? Title { get; set; }
    public int? CustomOrderValue { get; set; }

    public bool NoWrap { get; set; }

    public int RowSpan { get; set; }

    private int colSpan = 1;
    public int ColSpan
    {
        get
        {
            return colSpan < 1 ? 1 : colSpan;
        }
        set
        {
            colSpan = value > 0 ? value : 1;
        }
    }

    public int Index()
    {
        if (Parent == null) return -1;

        var result = 0;

        foreach (var cell in Parent.Cells)
        {
            if (cell == this) break;
            result += cell.ColSpan;
        }

        return result;
    }

    [JsonConstructor]
    public Cell(CellStyle baseStyle, List<CellPageValue> values, string identifier,
        bool hidden, string title, int? customOrderValue, bool noWrap, int rowSpan, int colSpan)
    {
        BaseStyle = baseStyle;
        Values = values;
        Identifier = identifier;
        Hidden = hidden;
        Title = title;
        CustomOrderValue = customOrderValue;
        NoWrap = noWrap;
        RowSpan = rowSpan;
        ColSpan = colSpan;
    }

    public Cell(List<CellValue> values, CellStyle? style = null, string? identifier = null)
    {
        BaseStyle = style ?? new CellStyle();

        Identifier = IdentityHelper.CreateIfNull(identifier, "c");

        foreach (var value in values)
        {
            Values.Add(new CellPageValue(value, null));
        }
    }

    public Cell(List<CellPageValue> values, CellStyle style, string? identifier = null)
    {
        BaseStyle = style;

        Identifier = IdentityHelper.CreateIfNull(identifier, "c");

        Values = values;
    }

    public bool CanPopup(int pageIndex)
    {
        return !string.IsNullOrWhiteSpace(Values[pageIndex].PopupTitle)
            || !string.IsNullOrWhiteSpace(Values[pageIndex].PopupText);
    }
    public virtual bool IsHidden(bool calculating = false)
    {
        bool hiddenCheck()
            => Values?.Count(x => !string.IsNullOrEmpty(x.Value.GetValue()?.ToString())) == 0;

        if (calculating)
        {
            return hiddenCheck();
        }
        else
        {
            return Hidden || hiddenCheck();
        }
    }

    public CellValue GetValue(int pageIndex)
    {
        if (Values.Count > pageIndex)
        {
            return Values[pageIndex].Value;
        }

        return Values[0].Value;
    }
    public string GetValueAsString(int pageIndex)
    {
        if (Values.Count > pageIndex)
        {
            return Values[pageIndex].Value.ToString() ?? string.Empty;
        }

        return Values.FirstOrDefault()?.Value?.ToString() ?? string.Empty;
    }
    public dynamic? GetOrderValue(int pageIndex)
    {
        if (CustomOrderValue.HasValue)
        {
            return CustomOrderValue.Value;
        }

        if (Values.Count > pageIndex)
        {
            return Values[pageIndex].AsOrderValue();
        }

        return Values[0].AsOrderValue();
    }

    public void SetStyle(CellStyle refStyle)
    {
        BaseStyle.FontColor = refStyle.FontColor ?? BaseStyle.FontColor;
        BaseStyle.BorderColor = refStyle.BorderColor ?? BaseStyle.BorderColor;
        BaseStyle.TextPosition = refStyle.TextPosition ?? BaseStyle.TextPosition;
        BaseStyle.BackgroundColor = refStyle.BackgroundColor ?? BaseStyle.BackgroundColor;

        //TODO: make better implementation
    }

    public CellStyle GetStyle(int pageIndex)
    {
        if (Values.Count > pageIndex)
        {
            return Values[pageIndex].Style ?? BaseStyle;
        }

        return BaseStyle;
    }
}