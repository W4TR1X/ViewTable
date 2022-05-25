namespace w4TR1x.ViewTable.Interfaces.Renderers;

public interface ITableRenderer<TOptions, T>
    where T : class
    where TOptions : class, ITableRendererOptions<T>
{
    public T? Content { get; set; }

    Task<T?> Render(Table table, int pageIndex, TOptions? rendererOptions);
}