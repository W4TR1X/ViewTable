using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using w4TR1x.ViewTable.Aspnet.Interfaces;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.Aspnet.Extensions
{
    public static class RowExtensions
    {
        public static TagBuilder Render(this IRow row, int renderIndex, TagBuilder builder = null)
        {
            var thisRow = new TagBuilder("tr");

            thisRow.Attributes.Add("id", row.Identifier);
            thisRow.AddCssClass($"vt-rl{row.Index()}");

            if (row.Collapsable && row.Rows.Any())
            {
                thisRow.AddCssClass("cursor-hand");

                thisRow.Attributes.Add("data-target", $".{row.Identifier}");
                thisRow.Attributes.Add("data-toggle", "collapse");
            }

            if (row.Parent != null)
            {
                if (row.Parent.Collapsable)
                {
                    thisRow.AddCssClass(row.Parent.Identifier);
                }

                if (row.Parent.Collapsed)
                {
                    thisRow.AddCssClass("collapse");
                }
            }

            if (row.Collapsed)
            {
                thisRow.AddCssClass("collapsed");
            }

            if (row.CanPopup())
            {
                if (row.Collapsable)
                {
                    throw new Exception("Don't use Collapsable and PopupText or PopupTitle together.");
                }

                thisRow.Attributes.Add("data-toggle", "popover");
                thisRow.Attributes.Add("data-trigger", "hover");
                thisRow.Attributes.Add("data-content", row.PopupText);
                thisRow.Attributes.Add("data-original-title", row.PopupTitle);
            }

            var rowIndex = row.Index();
            var index = 0;
            foreach (var cell in row.Cells)
            {
                var cellRender = cell.Render(renderIndex);

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

                if (row.Collapsable)
                {
                    if (index == 0)
                    {
                        cellRender.InnerHtml.AppendHtml(" <i class=\"fa text-muted\" aria-hidden=\"true\"></i>");
                    }
                }
                thisRow.InnerHtml.AppendHtml(cellRender);

                index++;
            }

            if (row.RowType == Enums.RowEnum.Header)
            {
                thisRow.AddCssClass("headerRow");
            }

            if (builder != null)
            {
                builder.InnerHtml.AppendHtml(thisRow);
            }

            foreach (var childRow in row.Rows)
            {
                childRow.Render(renderIndex, builder);
            }

            return thisRow;
        }
    }
}
