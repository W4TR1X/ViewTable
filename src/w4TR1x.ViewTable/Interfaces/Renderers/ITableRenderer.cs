namespace w4TR1x.ViewTable.Interfaces.Renderers;

public interface ITableRenderer<TOptions, T>
    where TOptions : class, ITableRendererOptions<T>
    where T : class
{
    public T? Content { get; set; }

    Task<T?> Render(Table table, int pageIndex, TOptions? rendererOptions);
}