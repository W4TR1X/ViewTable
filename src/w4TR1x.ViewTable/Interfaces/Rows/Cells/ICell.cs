using w4TR1x.ViewTable.Interfaces.Rows;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues.Values;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

namespace w4TR1x.ViewTable.Interfaces.Rows.Cells;

public interface ICell
{
    [JsonIgnore]
    IRow? Parent { get; set; }

    ICellStyle BaseStyle { get; set; }

    List<ICellPageValue> Values { get; }

    string Identifier { get; }

    bool NoWrap { get; set; }

    bool Hidden { get; set; }

    public string? Title { get; set; }
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

    ICellValue GetValue(int pageIndex);
    dynamic GetOrderValue(int pageIndex);
    string GetValueAsString(int pageIndex);
    bool CanPopup(int pageIndex);
    bool IsHidden(bool calculating = false);

    void SetStyle(ICellStyle refStyle);
}