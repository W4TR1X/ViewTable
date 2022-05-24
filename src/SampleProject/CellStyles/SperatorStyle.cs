using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Collections.Generic;
using w4TR1x.ViewTable.Interfaces;

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

        protected override void RenderExcelStyle(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue)
        {
            selectedRange.Merge = true;
            selectedRange.Worksheet.Row(selectedRange.Start.Row).Height = SPERATOR_HEIGTH;
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, ICell cell, ICellValue cellValue)
        {
            tagBuilder.Attributes.Add("style", $"height:{SPERATOR_HEIGTH}px;");

            foreach (var htmlClass in htmlClassList)
            {
                tagBuilder.AddCssClass(htmlClass);
            }
        }

    }
}
