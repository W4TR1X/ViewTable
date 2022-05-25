using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using w4TR1x.ViewTable.Interfaces.Rows.Cells;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.PageValues.Values;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

namespace w4TR1x.Aspnet.Interfaces
{
    public interface IHtmlStyle : ICellStyle
    {
        void Render(TagBuilder tagBuilder, ICell cell, ICellValue cellValue);
    }
}
