using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace w4TR1x.ViewTable
{
    public class Row : IRow
    {
        public List<ICell> Cells { get; }
        public List<IRow> Rows { get; private set; }
        public IRow Parent { get; set; }
        public List<ICellStyle> Styles { get; set; }


        public bool Orderable { get; set; }

        public List<double> CustomOrderValues { get; set; }


        public bool Collapsable { get; set; }
        public bool Collapsed { get; set; }

        public string Identifier { get; }

        public RowEnum RowType { get; set; }

        public string PopupTitle { get; set; }
        public string PopupText { get; set; }

        public Row(RowEnum type = RowEnum.Record, bool orderable = false, string identifier = "")
        {
            Orderable = orderable;

            CustomOrderValues = new List<double>();

            Styles = new List<ICellStyle>();

            RowType = type;
            Identifier = (identifier ?? "").Length == 0 ? $"row_{Guid.NewGuid().ToString().Replace("-", "")}" : identifier;
            Cells = new List<ICell>();
            Rows = new List<IRow>();
        }

        public int Index()
        {
            if (Parent != null)
            {
                return Parent.Index() + 1;
            }

            return 0;
        }

        public bool CanPopup()
        {
            return (PopupTitle != null && PopupTitle.Any()) || (PopupText != null && PopupText.Any());
        }

        public IRow UpdateColSpan(int colspan)
        {
            Cells.Last().ColSpan = colspan;

            return this;
        }

        public IRow UpdateRowSpan(int rowspan)
        {
            Cells.Last().RowSpan = rowspan;

            return this;
        }

        public IRow UpdateTitle(string title)
        {
            Cells.Last().Title = title;

            return this;
        }

        public IRow UpdateTextPosition(TextPositionEnum position)
        {
            Cells.Last().TextPosition = position;

            return this;
        }

        public IRow UpdateCellPopup(string popupTitle, string popupText)
        {
            Cells.Last().PopupTitle = popupTitle;
            Cells.Last().PopupText = popupText;

            return this;
        }

        public IRow AddCell(ICell column)
        {
            if (Rows.Count > 0)
                throw new Exception("Don't add cells after rows!");

            column.Parent = this;
            Cells.Add(column);

            return this;
        }
        public IRow AddCells(params ICell[] cells)
        {
            if (Rows.Count > 0)
                throw new Exception("Don't add cells after rows!");

            foreach (var cell in cells)
            {
                cell.Parent = this;
                Cells.Add(cell);
            }

            return this;
        }

        public IRow AddRow(IRow row)
        {
            var thisCellCount = row.CalculateCellArea();
            var rowCellCount = row.CalculateCellArea();

            if (thisCellCount == 0 || rowCellCount != thisCellCount)
                throw new Exception("Don't add rows before cells");

            row.Parent = this;

            Rows.Add(row);

            return this;
        }

        public string GetTitleFor(ICell cell)
        {
            if (cell.Title != null) return cell.Title;

            var index = Cells.IndexOf(cell);
            return GetTitleFor(index, Parent == null);
        }

        public string GetTitleFor(int index, bool searchSpread)
        {
            if (RowType == RowEnum.Header)
            {
                ICell cell = null;

                var cellId = 0;
                var cellIndex = -1;

                while (cell == null)
                {
                    cellIndex += Cells[cellId].ColSpan;

                    if (cellIndex >= index)
                    {
                        cell = Cells[cellId];
                        break;
                    }

                    cellId++;
                    if (cellId >= Cells.Count) break;
                }

                if (cell != null && cell.Values.Count > 0)
                {
                    return cell.Values[0].Value.ToString();
                }
            }

            if (searchSpread)
            {
                foreach (var row in Rows)
                {
                    var text = row.GetTitleFor(index, searchSpread);

                    if (text.Any()) return text;
                }
            }
            else
            {
                if (Parent == null)
                {
                    return GetTitleFor(index, true);
                }

                return Parent.GetTitleFor(index, false);
            }

            return "";
        }


        public void OrderBy(int cellIndex, int renderIndex, bool desc = false)
        {
            Rows.ForEach(row => row.OrderBy(cellIndex, renderIndex, desc));

            if (desc)
            {
                Rows = Rows.OrderByDescending(x => x.Orderable ? x.GetOrderValue(cellIndex, renderIndex) : null).ToList();
            }
            else
            {
                Rows = Rows.OrderBy(x => x.Orderable ? x.GetOrderValue(cellIndex, renderIndex) : null).ToList();
            }
        }


        public ICell GetCell(int cellIndex)
        {
            ICell cell = null;

            var totalCellArea = CalculateCellArea();

            if (totalCellArea > cellIndex)
            {
                var index = 0;
                var currentCellIndex = -1;
                do
                {
                    currentCellIndex += Cells[index].ColSpan;
                    index++;

                } while (currentCellIndex < cellIndex);

                cell = Cells[index - 1];
            }

            return cell;
        }

        public object GetValue(int cellIndex, int renderIndex)
        {      
            var cell = GetCell(cellIndex);

            if (cell != null)
            {
                return cell.GetValue(renderIndex);
            }

            return null;
        }

        public object GetOrderValue(int cellIndex, int renderIndex)
        {    
            if (CustomOrderValues.Count > renderIndex)
            {
                return CustomOrderValues[renderIndex];
            }

            var cell = GetCell(cellIndex);

            if (cell != null)
            {
                return cell.GetOrderValue(renderIndex);
            }

            return null;
        }

        public int CalculateCellArea()
        {
            return Cells.Sum(x => 1 + (x.ColSpan - 1));
        }

        public void TextCenterExceptFirstCell()
        {
            var cells = Cells.Skip(1).ToList();

            foreach (var cell in cells)
            {
                cell.TextPosition = TextPositionEnum.Center;
            }
        }

        public void Dispose()
        {

        }
    }
}
