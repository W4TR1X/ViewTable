using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.Style;
using System;
using System.Linq;
using w4TR1x.Excel.Extensions;
using w4TR1x.Excel.Interfaces;
using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Excel;
using w4TR1x.ViewTable.Interfaces.Rows.Cells;
using w4TR1x.ViewTable.Values;

namespace w4TR1x.Excel.Extensions
{
    public static class CellExtensions
    {
        public static void Render(this ICell cell, int renderIndex, ExcelRange range)
        {
            var cellValue = cell.GetValue(renderIndex);
            if (cellValue != null && cellValue.Value != null)
            {
                if (cellValue.Value is double dValue)
                {
                    if (dValue != 0)
                    {
                        range.Value = cellValue.Value;
                    }
                }
                else if (cellValue.Value is int iValue)
                {
                    if (iValue != 0)
                    {
                        range.Value = cellValue.Value;
                    }
                }
                else if (cellValue.Value is string sValue)
                {
                    range.Style.WrapText = true;
                    range.Value = cellValue.Value;
                }
                else
                {
                    range.Value = cellValue.Value;
                }

                if (cellValue is DoubleValue doubleValue)
                {
                    range.Style.Numberformat.Format = doubleValue.NumberFormat;
                }
                else if (cellValue.Value is DateTime)
                {
                    range.Style.Numberformat.Format = "dd/MM/yyyy";
                }
            }

            cell.Styles.Cast<ISheetStyle>().ToList().ForEach(style =>
            {
                if (style != null)
                {
                    style.Render(range, cell, cell.GetValue(renderIndex));

                    if (style.NumberFormat != null)
                    {
                        range.Style.Numberformat.Format = style.NumberFormat;
                    }
                }
            });

            if (cell.CanPopup())
            {
                range.AddComment($"{cell.PopupTitle}:\n{cell.PopupText}", "mustafa.dagci");
                range.Comment.AutoFit = true;
            }

            if (cell is ICustomSheetCell customCell)
            {
                customCell.Render(range, cell.Parent, cell);
            }

        }
    }
}
