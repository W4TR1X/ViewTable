using OfficeOpenXml;
using w4TR1x.ViewTable.Interfaces.Rows;
using w4TR1x.ViewTable.Interfaces.Rows.Cells;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues.Values;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

namespace w4TR1x.Excel.Interfaces
{
    public interface ISheetStyle : ICellStyle
    {
        string NumberFormat { get; }

        void Render(ExcelRange selectedRange, ICell cell, ICellValue cellValue);
        void Render(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue);
    }
}
