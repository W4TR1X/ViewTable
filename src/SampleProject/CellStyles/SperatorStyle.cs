namespace SampleProject.CellStyles
{
    public class SperatorStyle : HexelStyle
    {
        const int SPERATOR_HEIGTH = 5;

        List<string> htmlClassList = new List<string>()
        {
            "hidden",
            "p-0",
            "m-0",
            "bg-white",
            "border-right-0",
            "border-left-0"
        };

        public override string NumberFormat => null;

        protected override void RenderExcelStyle(ExcelRange selectedRange, Row row, Cell cell, CellValue cellValue)
        {
            selectedRange.Merge = true;
            selectedRange.Worksheet.Row(selectedRange.Start.Row).Height = SPERATOR_HEIGTH;
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, Cell cell, CellValue cellValue)
        {
            tagBuilder.Attributes.Add("style", $"height:{SPERATOR_HEIGTH}px;");

            foreach (var htmlClass in htmlClassList)
            {
                tagBuilder.AddCssClass(htmlClass);
            }
        }

    }
}
