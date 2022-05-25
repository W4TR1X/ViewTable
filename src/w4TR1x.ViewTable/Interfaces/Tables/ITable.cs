namespace w4TR1x.ViewTable.Interfaces.Tables;

public interface ITable
{
    string Identifier { get; }
    bool Responsive { get; set; }
    bool UseVerticalTable { get; set; }
    bool Stripped { get; set; }
    int FixedColumnCount { get; set; }

    List<Page> Pages { get; }

    List<IRow> Rows { get; }

    IRow? GetFirstRow() => Rows.FirstOrDefault();

    IRow? GetLastRow() => Rows.LastOrDefault();

    void AddRow(IRow row);
    void OrderBy(int cellIndex, int pageIndex, bool desc = false);

    Task<T?> Render<TOptions, T>(ITableRenderer<TOptions, T> renderer, int pageIndex, TOptions? rendererOptions) where T : class where TOptions : class, ITableRendererOptions<T>;
}