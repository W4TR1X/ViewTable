using w4TR1x.ViewTable.Values;

namespace w4TR1x.ViewTable;

[Serializable]
public class Row : IRow
{
    [JsonIgnore]
    public IRow Parent { get; set; }

    [JsonIgnore]
    public Table Table { get; private set; }

    public List<IRow> Rows { get; private set; }
    public List<ICell> Cells { get; }
    public List<ICellStyle> Styles { get; set; }
    public bool Orderable { get; set; }
    public List<double> CustomOrderValues { get; set; }
    public bool Collapsable { get; set; }
    public bool Collapsed { get; set; }
    public string Identifier { get; }
    public RowEnum RowType { get; set; }
    public string PopupTitle { get; set; }
    public string PopupText { get; set; }

    [JsonConstructor]
    public Row(List<IRow> rows, List<ICell> cells, List<ICellStyle> styles, bool orderable, List<double> customOrderValues,
        bool collapsable, bool collapsed, string ıdentifier, RowEnum rowType, string popupTitle, string popupText)
    {
        Rows = rows;
        Cells = cells;
        Styles = styles;
        Orderable = orderable;
        CustomOrderValues = customOrderValues;
        Collapsable = collapsable;
        Collapsed = collapsed;
        Identifier = ıdentifier;
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
            Cells = new List<ICell>();
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
            Rows = new List<IRow>();
        }
    }

    public Row(RowEnum type = RowEnum.Record, bool orderable = false, string identifier = "")
    {
        Orderable = orderable;

        CustomOrderValues = new List<double>();

        Styles = new List<ICellStyle>();

        RowType = type;
        Identifier = (identifier ?? "").Length == 0
            ? IdentityHelper.Create("r")
            : identifier;

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
        var cell = Cells.Last();

        cell.PopupTitle = popupTitle;
        cell.PopupText = popupText;

        return this;
    }

    public IRow UpdateWrap(bool noWrap = true)
    {
        Cells.Last().NoWrap = noWrap;

        return this;
    }

    public IRow AddCell(ICell cell)
    {
        if (Rows.Count > 0)
            throw new ArgumentOutOfRangeException(nameof(cell), "Don't add cells after rows!");

        cell.Parent = this;
        Cells.Add(cell);

        return this;
    }

    public IRow AddCells(params ICell[] cells)
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

    public IRow AddRow(IRow row)
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

    public string GetTitleFor(ICell cell)
    {
        if (cell.Title != null) return cell.Title;

        var index = Cells.IndexOf(cell);
        return GetTitleFor(index, Parent == null);
    }

    public string GetTitleFor(int index, bool searchSpread = false)
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
                return cell.Values[0].GetValue().ToString();
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
        if (Cells.Any())
        {
            var cells = Cells.Skip(1);

            Cells[0].TextPosition = TextPositionEnum.Left;
            foreach (var cell in cells)
            {
                cell.TextPosition = TextPositionEnum.Center;
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
            if (cell.Values.Count > pageIndex && cell.Values[pageIndex] is DoubleValue dValue && dValue.CalculateColumn != CalculateStyleEnum.None)
            {
                var retValue = CalculateFromColumn(pageIndex, cell, Rows, dValue.CalculateColumn, 0);
                dValue.Set(retValue);
            }
        }
    }

    private double CalculateFromColumn(int pageIndex, ICell cell, IList<IRow> rows, CalculateStyleEnum calculateStyle, double retValue)
    {
        foreach (var row in rows)
        {
            var rCell = row.GetCell(cell.Index());

            if (rCell.Values.Count == 1 && rCell.Values[0].Value is string)
            {
                retValue += CalculateFromColumn(pageIndex, cell, row.Rows, calculateStyle, retValue);
            }
            else
            {
                if (rCell != null && rCell.Values.Count > pageIndex && rCell.Values[pageIndex] is DoubleValue rValue)
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