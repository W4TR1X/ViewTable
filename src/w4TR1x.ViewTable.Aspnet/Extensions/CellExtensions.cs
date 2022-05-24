using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Interfaces;
using w4TR1x.Aspnet.Interfaces;
using w4TR1x.ViewTable.Aspnet;
using w4TR1x.Aspnet.Extensions;

namespace w4TR1x.Aspnet.Extensions
{
    public static class CellExtensions
    {
        public static TagBuilder Render(this ICell cell, int renderIndex, TagBuilder builder = null)
        {
            TagBuilder thisCell =
                cell.Parent.RowType switch
                {
                    RowEnum.Header => new TagBuilder("th"),
                    _ => new TagBuilder("td"),
                };

            thisCell.Attributes.Add("id", cell.Identifier);

            if (cell.Parent != null)
            {
                cell.Parent.Styles.Cast<IHtmlStyle>().ToList().ForEach(style =>
                {
                    style.Render(thisCell, cell, cell.GetValue(renderIndex));
                });
            }

            if (cell.ColSpan > 1)
            {
                thisCell.Attributes.Add("colspan", cell.ColSpan.ToString());
            }

            if (cell.RowSpan > 1)
            {
                thisCell.Attributes.Add("rowspan", cell.RowSpan.ToString());
            }

            var style =
                cell.Styles.Count > renderIndex ? (IHtmlStyle)cell.Styles[renderIndex] :
                cell.Values.Count == 1 ? (IHtmlStyle)cell.Styles[0] : null;

            if (style != null)
            {
                style.Render(thisCell, cell, cell.GetValue(renderIndex));
            }

            if (cell is ICustomHtmlCell customCell)
            {
                customCell.Render(thisCell, renderIndex, builder);
            }
            else
            {
                thisCell.AddCssClass("align-center");

                switch (cell.TextPosition)
                {
                    case TextPositionEnum.Center:
                        thisCell.AddCssClass("text-center");
                        thisCell.AddCssClass("center");
                        break;
                }

                var value = cell.GetValueAsString(renderIndex);

                value = value == "0" ? "" :
                        value == "0,0" ? "" :
                        value == "0,00" ? "" :
                        value == "0,000" ? "" :
                        value == "0" ? "" :
                        value == "0.0" ? "" :
                        value == "0.00" ? "" :
                        value == "0.000" ? "" : value;

                if (value != null && value != "")
                {
                    thisCell.InnerHtml.AppendHtml(value.Replace("\r\n", "<br>"));

                    if (cell.Parent != null && cell.Parent.RowType == RowEnum.Record)
                    {
                        thisCell.Attributes.Add("data-title", cell.Parent.GetTitleFor(cell));
                    }
                }
                else
                {
                    thisCell.AddCssClass("hidden");
                }
            }

            if (cell.CanPopup())
            {
                thisCell.Attributes.Add("data-toggle", "popover");
                thisCell.Attributes.Add("data-trigger", "hover");
                thisCell.Attributes.Add("data-content", cell.PopupText);
                thisCell.Attributes.Add("data-original-title", cell.PopupTitle);
            }

            return thisCell;
        }
    }
}
