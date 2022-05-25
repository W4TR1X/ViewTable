namespace w4TR1x.ViewTable.Interfaces.Renderers;

public interface ITableRendererOptions<T> where T : class
{
    public T Initialize(ITableRendererOptions<T>? baseOptions);
}
