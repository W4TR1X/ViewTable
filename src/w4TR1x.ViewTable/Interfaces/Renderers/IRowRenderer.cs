namespace w4TR1x.ViewTable.Interfaces.Renderers;

public interface IRowRenderer<TRow, T>
    where TRow : Row
    where T : class
{
    public void Render(TRow row, T value, IRowRendererOptions<T> options);
}