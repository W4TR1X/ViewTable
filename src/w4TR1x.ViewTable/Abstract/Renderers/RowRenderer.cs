namespace w4TR1x.ViewTable.Abstract.Renderers;

public abstract class RowRenderer<TRow, T> : IRowRenderer<TRow, T>
        where TRow : Row
        where T : class
{
    public abstract void Render(TRow row, T value, IRowRendererOptions<T> options);
}
