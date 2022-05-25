using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using w4TR1x.ViewTable.Interfaces.Cells;
using w4TR1x.ViewTable.Interfaces.Tables;

namespace w4TR1x.Aspnet.Interfaces
{
    public interface IHtmlStyle : ICellStyle
    {
        void Render(TagBuilder tagBuilder, ICell cell, ICellValue cellValue);
    }
}
