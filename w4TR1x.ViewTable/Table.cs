using w4TR1x.ViewTable.Base;
using w4TR1x.ViewTable.Enums;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace w4TR1x.ViewTable
{
    public class Table : EntryRow
    {
        public bool Responsive { get; set; }

        public Table(RowEnum rowType = RowEnum.Record, string identifier = "") : base(rowType, identifier)
        {
        }

        public override TagBuilder Render(TagBuilder builder = null)
        {
            TagBuilder div = new TagBuilder("div");

            div.Attributes.Add("id", "no-more-tables");

            div.AddCssClass("tab-pane");
            div.AddCssClass("fade");
            div.AddCssClass("show");
            div.AddCssClass("active");

            var thisTable = new TagBuilder("table");
            thisTable.Attributes.Add("id", Identifier + "-table");

            if (CanPopup())
            {
                thisTable.Attributes.Add("data-toggle", "popover");
                thisTable.Attributes.Add("data-trigger", "hover");
                thisTable.Attributes.Add("data-content", PopupText);
                thisTable.Attributes.Add("data-original-title", PopupTitle);
            }

            thisTable.AddCssClass("table");
            thisTable.AddCssClass("table-bordered");
            thisTable.AddCssClass("table-hover");
            thisTable.AddCssClass("small");
            thisTable.AddCssClass("col-md-12");
            thisTable.AddCssClass("table-condensed");
            thisTable.AddCssClass("cf");

            if (!Responsive)
                thisTable.AddCssClass("w-auto");

            var thisRow = new TagBuilder("tr");
            thisRow.Attributes.Add("id", Identifier);
            thisRow.AddCssClass($"vt-rl{Index()}");

            var index = 0;
            foreach (var cell in Cells)
            {
                var cellRender = cell.Render();

                if (Collapsable)
                {
                    if (index == 0)
                    {
                        cellRender.InnerHtml.AppendHtml(" <i class=\"fa text-muted\" aria-hidden=\"true\"></i>");
                    }
                }
                thisRow.InnerHtml.AppendHtml(cellRender);

                index++;
            }

            if (Cells.Count == Cells.Where(x => x.GetType() == typeof(HeaderCell)).Count())
            {
                thisRow.AddCssClass("headerRow");
            }

            thisTable.InnerHtml.AppendHtml(thisRow);

            div.InnerHtml.AppendHtml(thisTable);

            foreach (var row in Rows)
            {
                row.Render(thisTable);
            }

            return div;
        }
    }
}
