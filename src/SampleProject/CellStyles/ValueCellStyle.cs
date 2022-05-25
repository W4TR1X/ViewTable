using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using w4TR1x.ViewTable.Interfaces.Cells;
using w4TR1x.ViewTable.Interfaces.Tables;
using Color = System.Drawing.Color;

namespace SampleProject.CellStyles
{
    public class ValueCellStyle : HexelStyle
    {
        public override string NumberFormat => null;

        protected bool _textBold;

        Color borderColor = Color.FromArgb(128, 128, 128);

        public ValueCellStyle(bool textBold = false)
        {
            _textBold = textBold;
        }

        public ValueCellStyle()
        {

        }

        protected override void RenderExcelStyle(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue)
        {
            selectedRange.Style.Border.BorderAround(ExcelBorderStyle.Thin, borderColor);
            selectedRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            selectedRange.Worksheet.Row(selectedRange.Start.Row).Height = 20;

            selectedRange.Style.Font.Bold = _textBold;
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, ICell cell, ICellValue cellValue)
        {
            if (_textBold)
            {
                tagBuilder.AddCssClass("font-weight-bold");
            }
        }
    }
}