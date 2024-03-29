﻿using w4TR1x.ViewTable.Values;

namespace w4TR1x.ViewTable;

[Serializable]
public class Row
{
    [JsonIgnore]
    public Row? Parent { get; set; } = null!;

    [JsonIgnore]
    public Table Table { get; private set; } = null!;

    public List<Row> Rows { get; private set; }
    public List<Cell> Cells { get; }
    public List<CellStyle> Styles { get; set; }
    public bool Orderable { get; set; }
    public List<double> CustomOrderValues { get; set; }
    public bool Collapsable { get; set; }
    public bool Collapsed { get; set; }
    public string Identifier { get; }
    public RowEnum RowType { get; set; }

    public string? PopupTitle { get; set; }
    public string? PopupText { get; set; }

    [JsonConstructor]
    public Row(List<Row> rows, List<Cell> cells, List<CellStyle> styles, bool orderable, List<double> customOrderValues,
        bool collapsable, bool collapsed, string identifier, RowEnum rowType, string popupTitle, string popupText)
    {
        Rows = rows;
        Cells = cells;
        Styles = styles;
        Orderable = orderable;
        CustomOrderValues = customOrderValues;
        Collapsable = collapsable;
        Collapsed = collapsed;
        Identifier = identifier;
        RowType = rowType;
        PopupTitle = popupTitle;
        PopupText = popupText;

        if (Cells != null)
        {
            foreach (var cell in Cells)
            {
                cell.Parent = this;
            }
        }
        else
        {
            Cells = new List<Cell>();
        }

        if (Rows != null)
        {
            foreach (var row in Rows)
            {
                row.Parent = this;
            }
        }
        else
        {
            Rows = new List<Row>();
        }
    }

    public Row(RowEnum type = RowEnum.Record, bool orderable = false, string? identifier = null)
    {
        Orderable = orderable;

        CustomOrderValues = new List<double>();

        Styles = new List<CellStyle>();

        RowType = type;
        Identifier = IdentityHelper.CreateIfNull(identifier, "r");

        Cells = new List<Cell>();
        Rows = new List<Row>();
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

    public Row UpdateColSpan(int colspan)
    {
        Cells.Last().ColSpan = colspan;

        return this;
    }

    public Row UpdateRowSpan(int rowspan)
    {
        Cells.Last().RowSpan = rowspan;

        return this;
    }

    public Row UpdateTitle(string title)
    {
        Cells.Last().Title = title;

        return this;
    }

    public Row UpdateTextPosition(TextPositionEnum position)
    {
        var cell = Cells.Last();

        cell.BaseStyle.TextPosition = position;

        var cellValues = cell.Values
            .Where(x => x.Style?.TextPosition.HasValue == true);

        foreach (var value in cellValues)
        {
            value.Style!.TextPosition = position;
        }

        return this;
    }

    public Row UpdateCellPopup(string popupTitle, string popupText)
    {
        var cell = Cells.Last();

        foreach (var value in cell.Values)
        {
            value.PopupTitle = popupTitle;
            value.PopupText = popupText;
        }

        return this;
    }

    public Row UpdateWrap(bool noWrap = true)
    {
        Cells.Last().NoWrap = noWrap;

        return this;
    }

    public Row AddCell(Cell cell)
    {
        if (Rows.Count > 0)
            throw new ArgumentOutOfRangeException(nameof(cell), "Don't add cells after rows!");

        cell.Parent = this;
        Cells.Add(cell);

        return this;
    }

    public Row AddCells(params Cell[] cells)
    {
        if (Rows.Count > 0)
            throw new ArgumentOutOfRangeException(nameof(cells), "Don't add cells after rows!");

        foreach (var cell in cells)
        {
            cell.Parent = this;
            Cells.Add(cell);
        }

        return this;
    }

    public void SetTable(Table table)
    {
        this.Table = table;

        foreach (var row in Rows)
        {
            row.SetTable(table);
        }
    }

    public Row AddRow(Row row)
    {
        var thisCellCount = CalculateCellArea();
        var rowCellCount = row.CalculateCellArea();

        if (thisCellCount == 0 || rowCellCount != thisCellCount)
            throw new ArgumentOutOfRangeException(nameof(row), "Don't add rows before cells");

        row.Parent = this;
        row.SetTable(Table);

        Rows.Add(row);

        return this;
    }

    public string GetTitleFor(Cell cell)
    {
        if (cell.Title != null) return cell.Title;

        var index = Cells.IndexOf(cell);
        return GetTitleFor(index, Parent == null);
    }

    public string GetTitleFor(int index, bool searchSpread = false)
    {
        if (RowType == RowEnum.Header)
        {
            Cell? cell = null;

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
                return cell.Values[0].Value.GetValue()!.ToString();
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

        return string.Empty;
    }


    public void OrderBy(int cellIndex, int pageIndex, bool desc = false)
    {
        Rows.ForEach(row => row.OrderBy(cellIndex, pageIndex, desc));

        if (desc)
        {
            Rows = Rows.OrderByDescending(x => x.Orderable ? x.GetOrderValue(cellIndex, pageIndex) : null).ToList();
        }
        else
        {
            Rows = Rows.OrderBy(x => x.Orderable ? x.GetOrderValue(cellIndex, pageIndex) : null).ToList();
        }
    }


    public Cell GetCell(int cellIndex)
    {
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

            return Cells[index - 1];
        }

        throw new ArgumentOutOfRangeException(nameof(cellIndex));
    }

    public dynamic GetValue(int cellIndex, int pageIndex)
    {
        var cell = GetCell(cellIndex);

        if (cell != null)
        {
            return cell.GetValue(pageIndex);
        }

        throw new ArgumentOutOfRangeException(nameof(cellIndex));
    }

    public dynamic? GetOrderValue(int cellIndex, int pageIndex)
    {

        if (CustomOrderValues.Count > pageIndex)
        {
            return CustomOrderValues[pageIndex];
        }

        var cell = GetCell(cellIndex);

        if (cell != null)
        {
            return cell.GetOrderValue(pageIndex);
        }

        throw new ArgumentOutOfRangeException(nameof(cellIndex));
    }

    public int CalculateCellArea()
    {
        return Cells.Sum(x => 1 + (x.ColSpan - 1));
    }

    public void TextCenterExceptFirstCell()
    {
        if (Cells.Any())
        {
            var cells = Cells.Skip(1);

            Cells[0].SetStyle(new CellStyle()
            {
                TextPosition = TextPositionEnum.Left,
            });

            foreach (var cell in cells)
            {
                cell.SetStyle(new CellStyle()
                {
                    TextPosition = TextPositionEnum.Center,
                });
            }
        }
    }

    public void BeforeRender(int pageIndex)
    {
        foreach (var row in Rows)
        {
            row.BeforeRender(pageIndex);
        }

        foreach (var cell in Cells)
        {
            if (cell.Values.Count > pageIndex && cell.Values[pageIndex].Value is DoubleValue dValue && dValue.CalculateColumn != CalculateStyleEnum.None)
            {
                var retValue = CalculateFromColumn(pageIndex, cell, Rows, dValue.CalculateColumn, 0);
                dValue.Set(retValue);
            }
        }
    }

    private double CalculateFromColumn(int pageIndex, Cell cell, IList<Row> rows, CalculateStyleEnum calculateStyle, double retValue)
    {
        foreach (var row in rows)
        {
            var rCell = row.GetCell(cell.Index());

            if (rCell.Values.Count == 1 && rCell.Values[0].Value.Value is string)
            {
                retValue += CalculateFromColumn(pageIndex, cell, row.Rows, calculateStyle, retValue);
            }
            else
            {
                if (rCell.Values.Count > pageIndex && rCell.Values[pageIndex].Value is DoubleValue rValue)
                {
                    retValue += calculateStyle switch
                    {
                        CalculateStyleEnum.SumAll => (double)rValue.Value,
                        CalculateStyleEnum.SumPositiveOnly => (double)rValue.Value > 0 ? (double)rValue.Value : 0,
                        CalculateStyleEnum.SumNegativeOnly => (double)rValue.Value < 0 ? (double)rValue.Value : 0,
                        _ => 0
                    };
                }
            }
        }

        return retValue;
    }
}