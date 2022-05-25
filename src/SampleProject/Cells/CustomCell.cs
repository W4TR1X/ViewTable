using w4TR1x.Aspnet.Interfaces;
using w4TR1x.Excel.Interfaces;
using w4TR1x.ViewTable.Interfaces.Rows;
using w4TR1x.ViewTable.Interfaces.Rows.Cells;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues.Values;
using Color = System.Drawing.Color;

namespace SampleProject.Cells;

[Serializable]
public abstract class CustomCell : Cell, ICustomHtmlCell, ICustomSheetCell
{
    readonly Color borderColor = Color.FromArgb(128, 128, 128);

    protected CustomCell(string identifier = "") : base(values: new List<ICellValue>(), style: null, identifier)
    {

    }

    public virtual void Render(ExcelRange selectedRange, IRow row, ICell cell)
    {
        var horizontalAlignment = ExcelHorizontalAlignment.Left;

        switch (cell.TextPosition)
        {
            case TextPositionEnum.Left:
                horizontalAlignment = ExcelHorizontalAlignment.Left;
                break;
            case TextPositionEnum.Center:
                horizontalAlignment = ExcelHorizontalAlignment.Center;
                break;
            case TextPositionEnum.Right:
                horizontalAlignment = ExcelHorizontalAlignment.Right;
                break;
        }

        selectedRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, borderColor);
        selectedRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        selectedRange.Style.HorizontalAlignment = horizontalAlignment;
    }

    public abstract void Render(TagBuilder cellTagBuilder, int renderIndex, TagBuilder builder = null);
}
