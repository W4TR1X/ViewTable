using Microsoft.AspNetCore.Mvc.Rendering;
using w4TR1x.ViewTable.Aspnet;

namespace w4TR1x.ViewTable.Aspnet
{
    public static class TableExtentions
    {
        public static TagBuilder Render(this Table table, int renderIndex)
        {
            if (table.Pages.Count > renderIndex)
            {
                var page = table.Pages[renderIndex];
                if (page.OrderBy > -1)
                {
                    table.OrderBy(page.OrderBy, renderIndex, page.DescendingOrder);
                }

                var div = new TagBuilder("div");

                if (table.UseVerticalTable)
                {
                    div.Attributes.Add("id", "no-more-tables");
                }

                div.AddCssClass("tab-pane");
                div.AddCssClass("fade");
                div.AddCssClass("show");
                div.AddCssClass("active");

                var thisTable = new TagBuilder("table");
                thisTable.Attributes.Add("id", table.Identifier);

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

                div.InnerHtml.AppendHtml(thisTable);

                foreach (var row in table.Rows)
                {
                    row.Render(renderIndex, thisTable);
                }

                return div;

            }

            return null;
        }
    }
}
