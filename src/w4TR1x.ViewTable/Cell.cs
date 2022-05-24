namespace w4TR1x.ViewTable;

[Serializable]
public class Cell : ICell
{
    [JsonIgnore]
    public IRow? Parent { get; set; } = null;

    public ICellStyle BaseStyle { get; set; }
    public List<ICellPageValue> Values { get; } = new();

    public string Identifier { get; }
    public bool Hidden { get; set; }
    public string? Title { get; set; }
    public int? CustomOrderValue { get; set; }

    public bool NoWrap { get; set; }

    public int RowSpan { get; set; }

    [JsonIgnore]
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

    [JsonConstructor]
    public Cell(ICellStyle baseStyle, List<ICellPageValue> values, string identifier,
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

    public Cell(List<ICellValue> values, ICellStyle? style = null, string? identifier = null)
    {
        BaseStyle = style ?? new CellStyle();

        Identifier = IdentityHelper.CreateIfNull(identifier, "c");

        foreach (var value in values)
        {
            Values.Add(new CellPageValue(value, null));
        }
    }

    public Cell(List<ICellPageValue> values, ICellStyle style, string? identifier = null)
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

    public ICellValue GetValue(int pageIndex)
    {
        if (Values.Count > pageIndex)
        {
            return Values[pageIndex].Value;
        }

        return Values.First().Value;
    }
    public string GetValueAsString(int pageIndex)
    {
        if (Values.Count > pageIndex)
        {
            return Values[pageIndex].ToString() ?? string.Empty;
        }

        return Values.FirstOrDefault()?.ToString() ?? string.Empty;
    }
    public dynamic GetOrderValue(int pageIndex)
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

    public void SetStyle(ICellStyle refStyle)
    {
        BaseStyle.FontColor = refStyle.FontColor ?? BaseStyle.FontColor;
        BaseStyle.BorderColor = refStyle.BorderColor ?? BaseStyle.BorderColor;
        BaseStyle.TextPosition = refStyle.TextPosition ?? BaseStyle.TextPosition;
        BaseStyle.BackgroundColor = refStyle.BackgroundColor ?? BaseStyle.BackgroundColor;

        //TODO: make better implementation
    }
}