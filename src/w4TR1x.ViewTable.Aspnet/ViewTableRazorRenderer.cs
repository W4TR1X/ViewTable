using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using w4TR1x.ViewTable.Abstract.Renderers;
using w4TR1x.ViewTable.RazorTagHelper.Extensions;

namespace w4TR1x.ViewTable.RazorTagHelper
{
    public class ViewTableRazorRenderer : TableRenderer<RazorOptions, TagBuilder>
    {
        private readonly TagHelperOutput _output;

        public ViewTableRazorRenderer(TagHelperOutput output)
        {
            _output = output;
        }

        public override Task<TagBuilder?> Render(Table table, int pageIndex, RazorOptions? rendererOptions)
        {
            if (Content != null)
            {
                Content = null;
            }

            Content = (rendererOptions ?? new RazorOptions()).Initialize(DefaultOptions);

            if (table.Pages.Count > pageIndex)
            {
                var tableId = $"{table.Identifier}_{pageIndex}";

                var page = table.Pages[pageIndex];
                if (page.OrderBy > -1)
                {
                    table.OrderBy(page.OrderBy, pageIndex, page.DescendingOrder);
                }

                var div = new TagBuilder("div");
                _output.Content.SetHtmlContent(div);

                if (table.UseVerticalTable)
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

                if (table.Stripped)
                {
                    thisTable.AddCssClass("table-striped");
                }

                if (!table.Responsive) thisTable.AddCssClass("w-auto");

                table.Rows.ToList().ForEach(row =>
                {
                    row.BeforeRender(pageIndex);
                    row.Render(pageIndex, thisTable);
                });
            }

            return Task.FromResult(Content);
        }
    }
}