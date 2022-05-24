using w4TR1x.ViewTable.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w4TR1x.ViewTable.Interfaces
{
    public interface IRow : IDisposable
    {
        IRow Parent { get; set; }
        List<ICell> Cells { get; }
        List<IRow> Rows { get; }
        List<ICellStyle> Styles { get; set; }

        RowEnum RowType { get; set; }
        string Identifier { get; }

        bool Collapsable { get; set; }
        bool Collapsed { get; set; }

        bool Orderable { get; set; }
        List<double> CustomOrderValues { get; set; }

        IRow UpdateColSpan(int colspan);
        IRow UpdateRowSpan(int rowspan);
        IRow UpdateTextPosition(TextPositionEnum position);
        IRow UpdateCellPopup(string popupTitle, string popupText);

        IRow UpdateTitle(string title);


        void TextCenterExceptFirstCell();

        int CalculateCellArea();

        int Index();

        string GetTitleFor(ICell cell);

        //bool CalculateVisibilityOfColumn(int cellIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Index of cell</param>
        /// <param name="searchSpread">Searches same level and inner levels, by default false.</param>
        /// <returns></returns>
        string GetTitleFor(int index, bool searchSpread = false);

        bool CanPopup();

        //void HideCell(int cellIndex);

        object GetValue(int cellIndex, int renderIndex);
        object GetOrderValue(int cellIndex, int renderIndex);

        string PopupTitle { get; set; }
        string PopupText { get; set; }

        void OrderBy(int cellIndex, int renderIndex, bool desc = false);



        IRow AddCell(ICell column);
        IRow AddCells(ICell[] column);
        IRow AddRow(IRow row);

        //TagBuilder Render(TagBuilder builder = null);
    }
}
