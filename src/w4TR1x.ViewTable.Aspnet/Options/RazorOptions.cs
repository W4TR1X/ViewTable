namespace w4TR1x.ViewTable.RazorTagHelper.Options;

public class RazorOptions : ITableRendererOptions<TagBuilder>
{
    public TagBuilder Initialize(ITableRendererOptions<TagBuilder>? baseOptions)
    {
        var options = baseOptions as RazorOptions;

        //return options;

        return new TagBuilder("view-table");
    }
}