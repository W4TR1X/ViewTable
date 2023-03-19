namespace w4TR1x.ViewTable.Interfaces.Renderers;

public interface ICellRenderer<TCell, T>
    where TCell : Cell
    where T : class
{
    public void Render(TCell cell, T value, ICellRendererOptions<T> options);
}
