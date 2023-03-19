namespace w4TR1x.ViewTable;

[Serializable]
public class Table //: ITable
{
    public string Identifier { get; private set; } = null!;
    public bool Responsive { get; set; }
    public bool UseVerticalTable { get; set; }
    public bool Stripped { get; set; }
    public int FixedColumnCount { get; set; } = 1;

    public List<Page> Pages { get; private set; } = new();

    public List<Row> Rows { get; set; } = new();

    public Row? GetFirstRow() => Rows.FirstOrDefault();

    public Row? GetLastRow() => Rows.LastOrDefault();

    [JsonConstructor]
    public Table(string identifier, bool responsive, bool useVerticalTable, bool stripped,
        int fixedColumnCount, List<Page> pages, List<Row> rows)
    {
        Identifier = identifier;
        Responsive = responsive;
        UseVerticalTable = useVerticalTable;
        Stripped = stripped;
        FixedColumnCount = fixedColumnCount;

        if (pages != null)
        {
            foreach (var page in pages)
            {
                Pages.Add(page);
            }
        }

        if (pages != null)
        {
            foreach (var row in rows)
            {
                this.Rows.Add(row);
                row.SetTable(this);
            }
        }
    }

    public Table(string? identifier = null, params Page[] pages)
    {
        Init(identifier, pages);
    }

    public Table(params Page[] pages)
    {
        Init(null, pages);
    }

    void Init(string? identifier = null, params Page[] pages)
    {
        UseVerticalTable = true;

        if (pages.Length != 0)
        {
            Pages.AddRange(pages);
        }
        else
        {
            Pages.Add(new Page("Default"));
        }

        Identifier = IdentityHelper.CreateIfNull(identifier, "t");
    }

    public void AddRow(Row row)
    {
        row.SetTable(this);
        Rows.Add(row);
    }

    public void OrderBy(int cellIndex, int pageIndex, bool desc = false)
    {
        Rows.ToList().ForEach(row => row.OrderBy(cellIndex, pageIndex, desc));

        if (!desc)
        {
            Rows = Rows.OrderBy(x => x.GetOrderValue(cellIndex, pageIndex)).ToList();
        }
        else
        {
            Rows = Rows.OrderByDescending(x => x.GetOrderValue(cellIndex, pageIndex)).ToList();
        }
    }

    public Task<T?> Render<TOptions, T>(ITableRenderer<TOptions, T> renderer, int pageIndex, TOptions? rendererOptions) where T : class where TOptions : class, ITableRendererOptions<T>
        => renderer.Render(this, pageIndex, rendererOptions);
}