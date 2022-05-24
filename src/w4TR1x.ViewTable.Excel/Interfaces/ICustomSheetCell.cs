using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.ViewTable.Excel.Interfaces
{
    public interface ICustomSheetCell : ICell
    {
        void Render(ExcelRange selectedRange, IRow row, ICell cell);
    }
}
