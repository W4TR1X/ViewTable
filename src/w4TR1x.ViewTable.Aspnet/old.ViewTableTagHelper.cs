using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using w4TR1x.ViewTable.RazorTagHelper.Extensions;

namespace w4TR1x.ViewTable.RazorTagHelper;

[HtmlTargetElement("view-table", Attributes = nameof(Table), TagStructure = TagStructure.NormalOrSelfClosing)]
public class ViewTableTagHelper : TagHelper
{
    public Table Table { get; set; }

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

        if (Table.Pages.Count > RenderIndex)
        {
            var tableId = $"{Table.Identifier}_{RenderIndex}";

            var page = Table.Pages[RenderIndex];
            if (page.OrderBy > -1)
            {
                Table.OrderBy(page.OrderBy, RenderIndex, page.DescendingOrder);
            }

            var div = new TagBuilder("div"); output.Content.SetHtmlContent(div);

            if (Table.UseVerticalTable)
            {
                div.Attributes.Add("id", "no-more-tables");
            }

            div.Attributes.Add("style", "overflow-x:auto;max-height: 70vh;");

            div.AddCssClass("tab-pane");
            div.AddCssClass("fade");
            div.AddCssClass("show");
            div.AddCssClass("active");

            var thisTable = new TagBuilder("table"); div.InnerHtml.AppendHtml(thisTable);

            thisTable.Attributes.Add("id", tableId);

            thisTable.AddCssClass("table");
            thisTable.AddCssClass("table-bordered");
            thisTable.AddCssClass("table-hover");
            thisTable.AddCssClass("small");
            thisTable.AddCssClass("col-md-12");
            thisTable.AddCssClass("table-condensed");
            thisTable.AddCssClass("cf");
            thisTable.AddCssClass("mb-0");

            if (Table.Stripped)
            {
                thisTable.AddCssClass("table-striped");
            }

            if (!Table.Responsive) thisTable.AddCssClass("w-auto");

            Table.Rows.ToList().ForEach(row =>
            {
                row.BeforeRender(RenderIndex);
                row.Render(RenderIndex, thisTable);
            });
        }

        return Task.CompletedTask;
    }
}
