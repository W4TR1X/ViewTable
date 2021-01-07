using Microsoft.AspNetCore.Mvc.Rendering;
//using OfficeOpenXml;
using System.Drawing;
using System.Linq;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.ViewTable
{
    public class TotalCell : Cell//, IHasExcelCellStyle
    {
        public bool FillWhenEmpty { get; set; }

        public TotalCell(string text, bool fillWhenEmpty = true) : base(text)
        {
            FillWhenEmpty = fillWhenEmpty;
        }

        public override TagBuilder Render(TagBuilder builder = null)
        {
            var render = base.Render(builder);

            if (FillWhenEmpty || (Text != null && Text.Any()))
            {
                render.AddCssClass("bg-yellow");
            }

            render.AddCssClass("font-weight-bold");

            return render;
        }

        //EPPlus styling
        //public void Style(ExcelRange range)
        //{
        //    range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
        //}
    }
}
