namespace w4TR1x.ViewTable;

[Serializable]
public class Cell : ICell
{
    [JsonIgnore]
    public IRow? Parent { get; set; } = null;

    public ICellStyle BaseStyle { get; set; }
    public List<ICellValue> Values { get; }

    public string Identifier { get; }
    public bool Hidden { get; set; }
    public string Title { get; set; }
    public string PopupTitle { get; set; }
    public string PopupText { get; set; }
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
    public Cell(ICellStyle baseStyle, List<ICellValue> values,
        string identifier, bool hidden, string title, string popupTitle, string popupText,
        int? customOrderValue, bool noWrap, int rowSpan, int colSpan)
    {
        BaseStyle = baseStyle;

        Values = values;
        Identifier = identifier;
        Hidden = hidden;
        Title = title;
        PopupTitle = popupTitle;
        PopupText = popupText;
        CustomOrderValue = customOrderValue;
        NoWrap = noWrap;
        RowSpan = rowSpan;
        ColSpan = colSpan;
    }

    public Cell(List<ICellValue> values, ICellStyle style, string? identifier = null)
    {
        BaseStyle = style;

        Identifier = string.IsNullOrWhiteSpace(identifier)
            ? IdentityHelper.Create("c")
            : identifier;

        Values = values;
    }

    public bool CanPopup()
    {
        return (PopupTitle != null && PopupTitle.Any()) || (PopupText != null && PopupText.Any());
    }
    public virtual bool IsHidden(bool calculating = false)
    {
        bool hiddenCheck()
            => Values?.Count(x => !string.IsNullOrEmpty(x.GetValue().ToString())) == 0;

        if (calculating)
        {
            return hiddenCheck();
        }
        else
        {
            return Hidden || hiddenCheck();
        }
    }

    public ICellValue GetValue(int renderIndex)
    {
        if (Values.Count > renderIndex)
        {
            return Values[renderIndex];
        }

        return Values.First();
    }
    public string GetValueAsString(int renderIndex)
    {
        if (Values.Count > renderIndex)
        {
            return Values[renderIndex].ToString() ?? string.Empty;
        }

        return Values.FirstOrDefault()?.ToString() ?? string.Empty;
    }
    public object GetOrderValue(int renderIndex)
    {
        if (CustomOrderValue.HasValue)
        {
            return CustomOrderValue.Value;
        }

        if (Values.Count > renderIndex)
        {
            return Values[renderIndex].AsOrderValue();
        }

        return Values[0].AsOrderValue();
    }
}