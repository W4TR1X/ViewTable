namespace w4TR1x.ViewTable.Abstract.Renderers;

public abstract class TableRenderer<TOptions, TResult> : ITableRenderer<TOptions, TResult>
        where TResult : class
        where TOptions : class, ITableRendererOptions<TResult>
{
    public TResult? Content { get; set; }

    public TOptions? DefaultOptions { get; set; }

    protected TableRenderer(TOptions defaultOptions)
    {
        DefaultOptions = defaultOptions;
    }

    protected TableRenderer()
    { }

    public abstract Task<TResult?> Render(Table table, int pageIndex, TOptions? rendererOptions);
}
