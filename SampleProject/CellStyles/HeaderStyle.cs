using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Interfaces;

namespace Efesan.Aspnet.Common.CellStyles
{
    public class HeaderStyle : HexelStyle
    {
        List<string> htmlClassList = new List<string>()
        {
            "font-weight-bold",
            "center",
            "sticky-header"
        };

        public int TopOffset { get; set; }

        public HeaderStyle(int topOffset = 59)
        {
            TopOffset = topOffset;
        }

        public HeaderStyle()
        {
            TopOffset = 59;
        }

        public override string NumberFormat => null;

        Color borderColor = Color.FromArgb(128, 128, 128);
        Color bgColor = Color.FromArgb(236, 252, 255);

        protected override void RenderExcelStyle(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue)
        {
            selectedRange.Style.Border.BorderAround(ExcelBorderStyle.Medium, borderColor);
            selectedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            selectedRange.Style.Fill.BackgroundColor.SetColor(bgColor);
            selectedRange.Style.Font.Bold = true;
            selectedRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, ICell cell, ICellValue cellValue)
        {
            if (cell.TextPosition == TextPositionEnum.Left)
            {
                htmlClassList.Remove("center");
            }

            foreach (var htmlClass in htmlClassList)
            {
                tagBuilder.AddCssClass(htmlClass);
            }

            tagBuilder.MergeAttribute("style", $"top:{TopOffset}px;");
        }
    }
}
