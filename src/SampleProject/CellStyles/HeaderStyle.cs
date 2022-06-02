using Color = System.Drawing.Color;

namespace SampleProject.CellStyles
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

        protected override void RenderExcelStyle(ExcelRange selectedRange, Row row, Cell cell, CellValue cellValue)
        {
            selectedRange.Style.Border.BorderAround(ExcelBorderStyle.Medium, borderColor);
            selectedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            selectedRange.Style.Fill.BackgroundColor.SetColor(bgColor);
            selectedRange.Style.Font.Bold = true;
            selectedRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, Cell cell, CellValue cellValue)
        {
            //if (cell.TextPosition == TextPositionEnum.Left)
            //{
            //    htmlClassList.Remove("center");
            //}

            foreach (var htmlClass in htmlClassList)
            {
                tagBuilder.AddCssClass(htmlClass);
            }

            tagBuilder.MergeAttribute("style", $"top:{TopOffset}px;");
        }
    }
}
