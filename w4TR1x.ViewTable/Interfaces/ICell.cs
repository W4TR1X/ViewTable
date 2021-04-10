using System;
using System.Collections.Generic;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.Interfaces
{
    public interface ICell : IDisposable
    {
        IRow Parent { get; set; }
        List<ICellStyle> Styles { get; set; }

        List<ICellValue> Values { get; }
        string Identifier { get; }


        bool Hidden { get; set; }


        string PopupTitle { get; set; }
        string PopupText { get; set; }

        int Index => Parent != null ? Parent.Cells.IndexOf(this) : -1;

        public string Title { get; set; }


        int? CustomOrderValue { get; set; }

        int ColSpan { get; set; }
        int RowSpan { get; set; }


        ICellValue GetValue(int renderIndex);
        object GetOrderValue(int renderIndex);
        string GetValueAsString(int renderIndex);

        TextPositionEnum TextPosition { get; set; }

        bool CanPopup();
        bool IsHidden(bool calculating = false);
    }
}
