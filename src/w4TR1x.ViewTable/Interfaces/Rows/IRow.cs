using w4TR1x.ViewTable.Interfaces.Rows.Cells;
using w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

namespace w4TR1x.ViewTable.Interfaces.Rows;

public interface IRow
{
    [JsonIgnore]
    IRow Parent { get; set; }

    List<IRow> Rows { get; }

    List<ICell> Cells { get; }
    List<ICellStyle> Styles { get; set; }
    RowEnum RowType { get; set; }
    string Identifier { get; }
    bool Collapsable { get; set; }
    bool Collapsed { get; set; }
    bool Orderable { get; set; }
    List<double> CustomOrderValues { get; set; }

    string? PopupTitle { get; set; }
    string? PopupText { get; set; }

    void SetTable(Table table);

    [JsonIgnore]
    Table Table { get; }

    IRow UpdateColSpan(int colspan);
    IRow UpdateRowSpan(int rowspan);
    IRow UpdateWrap(bool noWrap = true);
    IRow UpdateTextPosition(TextPositionEnum position);
    IRow UpdateCellPopup(string popupTitle, string popupText);

    IRow UpdateTitle(string title);

    void TextCenterExceptFirstCell();

    int CalculateCellArea();

    int Index();

    string GetTitleFor(ICell cell);

    string GetTitleFor(int index, bool searchSpread = false);

    bool CanPopup();

    dynamic GetValue(int cellIndex, int pageIndex);
    dynamic GetOrderValue(int cellIndex, int pageIndex);

    void OrderBy(int cellIndex, int pageIndex, bool desc = false);

    void BeforeRender(int pageIndex);

    IRow AddCell(ICell cell);
    IRow AddCells(ICell[] cells);

    ICell GetCell(int cellIndex);

    IRow AddRow(IRow row);
}