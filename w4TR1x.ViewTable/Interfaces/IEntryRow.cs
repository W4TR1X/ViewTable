using w4TR1x.ViewTable.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w4TR1x.ViewTable.Interfaces
{
    public interface IEntryRow : IEntryCellStyle
    {
        public List<IEntryCell> Cells { get; }
        public List<IEntryRow> Rows { get; }

        public RowEnum RowType { get; set; }
        public string Identifier { get; }

        public bool Collapsable { get; set; }
        public bool Collapsed { get; set; }

        public int Index();
        public string GetTitleFor(IEntryCell cell);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Index of cell</param>
        /// <param name="searchSpread">Searches same level and inner levels, by default false.</param>
        /// <returns></returns>
        public string GetTitleFor(int index, bool searchSpread = false);


        public void AddCell(IEntryCell column);
        public void AddRow(IEntryRow row);
    }
}
