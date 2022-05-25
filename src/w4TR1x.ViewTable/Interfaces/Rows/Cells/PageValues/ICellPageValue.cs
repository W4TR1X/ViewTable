using w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues.Values;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

namespace w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues;

public interface ICellPageValue
{
    ICellValue Value { get; set; }
    ICellStyle? Style { get; set; }

    string? PopupTitle { get; set; }
    string? PopupText { get; set; }

    dynamic AsOrderValue();
}
