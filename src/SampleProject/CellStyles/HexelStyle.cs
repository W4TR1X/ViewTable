using Microsoft.AspNetCore.Mvc.Rendering;
using w4TR1x.ViewTable.Aspnet.Interfaces;
using w4TR1x.ViewTable.Excel.Interfaces;
using w4TR1x.ViewTable.Interfaces;
using System.Collections.Generic;
using w4TR1x.ViewTable.Values;
using w4TR1x.ViewTable;
using OfficeOpenXml;
using System;
using OfficeOpenXml.Style;
using w4TR1x.ViewTable.Enums;

namespace Efesan.Aspnet.Common.CellStyles
{
    public abstract class HexelStyle : ISheetStyle, IHtmlStyle
    {
        public abstract string NumberFormat { get; }

        //public void CreateStyledCells(IRow row, params object[] cellValues)
        //{
        //    var values = new List<ICellValue>();
        //    var styles = new List<ICellStyle>();

        //    foreach (var param in cellValues)
        //    {
        //        if (param is string stringParam)
        //        {
        //            values.Add(new StringValue(stringParam));
        //        }
        //        else if (param is int intParam)
        //        {
        //            values.Add(new IntValue(intParam));
        //        }
        //        else if (param is DateTime dateParam)
        //        {
        //            values.Add(new DateValue(dateParam));
        //        }
        //        else
        //        {
        //            throw new ArgumentOutOfRangeException(nameof(param));
        //        }

        //        styles.Add(this);
        //    }

        //    row.AddCell(new Cell(values, styles));
        //}

        #region EXCEL

        public void Render(ExcelRange selectedRange, ICell cell, ICellValue cellValue)
        {
            setPredefinedOptions(selectedRange, cell, cellValue);
            RenderExcelStyle(selectedRange, null, cell, cellValue);
        }

        public void Render(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue)
        {
            setPredefinedOptions(selectedRange, cell, cellValue);
            RenderExcelStyle(selectedRange, row, cell, cellValue);
        }

        void setPredefinedOptions(ExcelRange selectedRange, ICell cell, ICellValue cellValue)
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

            selectedRange.Style.HorizontalAlignment = horizontalAlignment;

            var numberFormat = "General";

            if(cellValue is DoubleValue doubleValue)
            {
                numberFormat = doubleValue.NumberFormat;
            }

            if (cellValue is DateValue)
            {
                numberFormat = "dd/MM/yyyy";
            }

            if (cellValue is DecoratedDoubleValue decorated)
            {
                if (decorated.BeforeText != null)
                {
                    numberFormat = $"\"{decorated.BeforeText} \"{numberFormat}";
                }

                if (decorated.AfterText != null)
                {
                    numberFormat = $"{numberFormat}\" {decorated.AfterText}\"";
                }
            }

            selectedRange.Style.Numberformat.Format = numberFormat;
        }

        protected abstract void RenderExcelStyle(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue);

        #endregion

        #region HTML

        public void Render(TagBuilder tagBuilder, ICell cell, ICellValue cellValue)
        {
            RenderHtmlStyle(tagBuilder, cell, cellValue);
        }

        public abstract void RenderHtmlStyle(TagBuilder tagBuilder, ICell cell, ICellValue cellValue);

        #endregion

        protected decimal calculateCellValue(ICellValue cellValue)
        {
            if (cellValue.Value is int intValue)
            {
                return intValue;
            }
            else if (cellValue.Value is double doubleValue)
            {
                return (decimal)doubleValue;
            }
            else if (cellValue.Value is decimal decimalValue)
            {
                return decimalValue;
            }

            return 0;
        }

        public void Dispose()
        {

        }
    }
}
