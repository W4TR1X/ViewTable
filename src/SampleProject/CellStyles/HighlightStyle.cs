using Color = System.Drawing.Color;

namespace SampleProject.CellStyles
{
    public class HighlightStyle : HexelStyle
    {
        List<string> htmlClassList = new List<string>()
        {
            "font-weight-bold",
            "bg-yellow",
            "center"
        };

        public override string NumberFormat => null;

        Color borderColor = Color.FromArgb(128, 128, 128);
        Color bgColor = Color.FromArgb(252, 255, 180);

        protected override void RenderExcelStyle(ExcelRange selectedRange, Row row, Cell cell, CellValue cellValue)
        {
            selectedRange.Style.Border.BorderAround(ExcelBorderStyle.Medium, borderColor);
            selectedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            selectedRange.Style.Fill.BackgroundColor.SetColor(bgColor);
            selectedRange.Style.Font.Bold = true;
            selectedRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            selectedRange.Worksheet.Row(selectedRange.Start.Row).Height = 20;
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, Cell cell, CellValue cellValue)
        {
            //if (cell.TextPosition != TextPositionEnum.Center)
            //{
            //    htmlClassList.Remove("center");
            //}

            foreach (var htmlClass in htmlClassList)
            {
                tagBuilder.AddCssClass(htmlClass);
            }
        }
    }
}
