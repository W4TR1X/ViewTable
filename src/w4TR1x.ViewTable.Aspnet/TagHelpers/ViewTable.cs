using Microsoft.AspNetCore.Razor.TagHelpers;

namespace w4TR1x.ViewTable.RazorTagHelper.TagHelpers;

[HtmlTargetElement("view-table", Attributes = nameof(Table), TagStructure = TagStructure.NormalOrSelfClosing)]
public class ViewTableTagHelper : TagHelper
{
    public Table? Table { get; set; }

    public ViewTableRazorRenderer? Renderer { get; set; }
    public RazorOptions? RenderOptions { get; set; }

    public int RenderIndex { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (Table == null)
        {
            output.SuppressOutput();
            return Task.CompletedTask;
        }

        if (context == null) throw new ArgumentNullException(nameof(context));
        if (output == null) throw new ArgumentNullException(nameof(output));

        var renderer = Renderer ?? new ViewTableRazorRenderer(output);

        return renderer.Render(Table, RenderIndex, RenderOptions);
    }
}