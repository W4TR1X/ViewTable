namespace w4TR1x.ViewTable.Abstract.Renderers;

public abstract class CellRenderer<TCell, T> : ICellRenderer<TCell, T>
        where TCell : Cell
        where T : class
{
    public abstract void Render(TCell cell, T value, ICellRendererOptions<T> options);
}