using Microsoft.AspNetCore.Mvc.Rendering;
//using OfficeOpenXml;
using System.Drawing;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.ViewTable
{
    public class HeaderCell : Cell//, IHasExcelCellStyle
    {
        public HeaderCell(string text) : base(text)
        {
        }

        public override TagBuilder Render(TagBuilder builder = null)
        {
            var render = base.Render(builder);

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
