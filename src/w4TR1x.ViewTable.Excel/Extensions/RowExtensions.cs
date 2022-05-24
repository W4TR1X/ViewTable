using OfficeOpenXml;
using System.Linq;
using w4TR1x.ViewTable.Excel.Interfaces;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.ViewTable.Excel
{
    public static class RowExtensions
    {
        public static void Render(this IRow row, int renderIndex, ExcelWorksheet sheet, ref int rowIndex)
        {
            var localRowIndex = rowIndex;

            var usedCellCount = row.CalculateCellArea();

            var childRowStartIndex = rowIndex + 1;

            row.Styles.Cast<ISheetStyle>().ToList().ForEach(style =>
            {
                style.Render(sheet.Cells[localRowIndex, 1, localRowIndex, usedCellCount], row, null, null);
            });

            var cellIndex = 1;
            foreach (var cell in row.Cells)
            {
                var range = sheet.Cells[localRowIndex, cellIndex, rowIndex, cellIndex + (cell.ColSpan - 1)];

                if (cell.ColSpan > 1)
                {
                    range.Merge = true;
                }

                cell.Render(renderIndex, range);

                cellIndex += cell.ColSpan;
            }

            rowIndex++;

            foreach (var childRow in row.Rows)
            {
                childRow.Render(renderIndex, sheet, ref rowIndex);
            }

            if (row.Collapsable && childRowStartIndex < rowIndex)
            {
                for (int i = childRowStartIndex; i < rowIndex; i++)
                {
                    sheet.Row(i).OutlineLevel = 1;
                    sheet.Row(i).Collapsed = row.Collapsed;
                }
            }

            if (row.RowType == Enums.RowEnum.Header)
            {
                var range = sheet.Cells[localRowIndex, 1, localRowIndex, usedCellCount];

                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                range.AutoFilter = true;

                sheet.Row(localRowIndex).Height = 120;
                range.AutoFitColumns(50);
                range.AutoFitColumns();

                var maxRows = 1;

                row.Cells.ForEach(cell =>
                {
                    var count = cell.GetValueAsString(renderIndex).Count(x => x == '\n') + 1;
                    if (count > maxRows)
                    {
                        maxRows = count;
                    }
                });

                var exRow = sheet.Row(localRowIndex);

                exRow.Height = maxRows * 15;
                if (exRow.Height == 15) exRow.Height = 20;

                sheet.View.FreezePanes(localRowIndex + 1, 1);
            }
        }
    }
}
