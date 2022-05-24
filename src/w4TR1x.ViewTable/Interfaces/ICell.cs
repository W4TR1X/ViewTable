namespace w4TR1x.ViewTable.Interfaces;

public interface ICell
{
    [JsonIgnore]
    IRow? Parent { get; set; }

    ICellStyle BaseStyle { get; set; }

    List<ICellValue> Values { get; }

    string Identifier { get; }

    bool NoWrap { get; set; }

    bool Hidden { get; set; }

    public string Title { get; set; }
    int? CustomOrderValue { get; set; }

    int ColSpan { get; set; }
    int RowSpan { get; set; }

    int Index()
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

    ICellValue GetValue(int renderIndex);
    dynamic GetOrderValue(int renderIndex);
    string GetValueAsString(int renderIndex);
    bool CanPopup();
    bool IsHidden(bool calculating = false);
}