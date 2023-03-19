using w4TR1x.ViewTable.Abstract.Renderers;

namespace w4TR1x.Excel;

public class ViewTableExcelRenderer : TableRenderer<WorkbookOptions, ExcelPackage>
{
    public override Task<ExcelPackage?> Render(Table table, int pageIndex, WorkbookOptions? rendererOptions)
    {
        if (Content != null)
        {
            Content.Dispose();
            Content = null;
        }

        Content = rendererOptions?.Initialize(DefaultOptions);



        return Task.FromResult(Content);
    }
}
