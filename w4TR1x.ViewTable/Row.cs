using w4TR1x.ViewTable.Base;
using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace w4TR1x.ViewTable
{
    public class Row : EntryRow
    {
        public Row(RowEnum rowType = RowEnum.Record, string identifier = "") : base(rowType, identifier)
        {
        }

        public override TagBuilder Render(TagBuilder builder = null)
        {
            TagBuilder thisRow = new TagBuilder("tr");

            thisRow.Attributes.Add("id", Identifier);
            thisRow.AddCssClass($"vt-rl{Index()}");

            if (Collapsable && Rows.Any())
            {
                thisRow.AddCssClass("cursor-hand");

                thisRow.Attributes.Add("data-target", $".{Identifier}");
                thisRow.Attributes.Add("data-toggle", "collapse");
            }

            if (Parent.Collapsable)
            {
                thisRow.AddCssClass(Parent.Identifier);
            }

            if (Parent.Collapsed)
            {
                thisRow.AddCssClass("collapse");
            }

            if (Collapsed)
            {
                thisRow.AddCssClass("collapsed");
            }

            if (CanPopup())
            {
                if (Collapsable)
                {
                    throw new Exception("Don't use Collapsable and PopupText or PopupTitle together.");
                }

                thisRow.Attributes.Add("data-toggle", "popover");
                thisRow.Attributes.Add("data-trigger", "hover");
                thisRow.Attributes.Add("data-content", PopupText);
                thisRow.Attributes.Add("data-original-title", PopupTitle);
            }

            var rowIndex = Index();
            var index = 0;
            foreach (var cell in Cells)
            {
                var cellRender = cell.Render();

                if (index == 0 && rowIndex >= 3)
                {
                    var indicator = " ";
                    for (var i = 3; i < rowIndex; i++)
                    {
                        indicator = " " + indicator;
                    }

                    var cellValue = new TagBuilder("none");
                    cellRender.InnerHtml.CopyTo(cellValue.InnerHtml);

                    cellRender.InnerHtml.Clear();
                    cellRender.InnerHtml.Append(indicator);

                    cellValue.InnerHtml.CopyTo(cellRender.InnerHtml);
                }

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

            if (builder != null)
            {
                builder.InnerHtml.AppendHtml(thisRow);
            }

            foreach (var row in Rows)
            {
                row.Render(builder);
            }

            return thisRow;
        }
    }
}
