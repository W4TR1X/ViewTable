using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace w4TR1x.ViewTable.Base
{
    public abstract class EntryRow : IEntryRow
    {
        public List<IEntryCell> Cells { get; }
        public List<IEntryRow> Rows { get; }
        public IEntryRow Parent { get; set; }

        public bool Collapsable { get; set; }
        public bool Collapsed { get; set; }

        public string Identifier { get; }

        public RowEnum RowType { get; set; }

        public string PopupTitle { get; set; }
        public string PopupText { get; set; }

        public string BackgroundStyleClass { get; set; }
        public string FontStyleClass { get; set; }

        public int Index()
        {
            if(Parent!=null)
            {
                return Parent.Index() + 1;
            }

            return 0;
        }

        public bool CanPopup()
        {
            return (PopupTitle != null && PopupTitle.Any()) || (PopupText != null && PopupText.Any());
        }

        public EntryRow(RowEnum type = RowEnum.Record, string identifier = "")
        {
            RowType = type;
            Identifier = (identifier ?? "").Length == 0 ? $"row_{Guid.NewGuid().ToString().Replace("-", "")}" : identifier;
            Cells = new List<IEntryCell>();
            Rows = new List<IEntryRow>();
        }

        public void AddCell(IEntryCell column)
        {
            if (Rows.Count > 0)
                throw new Exception("Don't add cells after rows!");

            column.Parent = this;
            Cells.Add(column);
        }

        public void AddRow(IEntryRow row)
        {
            if (Cells.Count == 0 || row.Cells.Count != Cells.Count)
                throw new Exception("Don't add rows before cells and ");

            row.Parent = this;
            Rows.Add(row);
        }


        public abstract TagBuilder Render(TagBuilder builder = null);

        public string GetTitleFor(IEntryCell cell)
        {
            var index = Cells.IndexOf(cell);
            return GetTitleFor(index, Parent == null);
        }

        public string GetTitleFor(int index, bool searchSpread)
        {
            if (RowType == RowEnum.Header)
            {
                return Cells[index].Text;
            }

            if (searchSpread)
            {
                foreach (var row in Rows)
                {
                    var text = row.GetTitleFor(index, searchSpread);
                    if (text.Any())
                        return text;
                }
            }
            else
            {
                if (Parent == null)
                {
                    return "";
                }

                return Parent.GetTitleFor(index);
            }

            return "";
        }
    }
}
