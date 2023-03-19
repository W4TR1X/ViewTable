using System.Linq;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.RazorTagHelper.Extensions;

public static class CellExtensions
{
    public static TagBuilder Render(this Cell cell, int renderIndex, TagBuilder builder = null)
    {
        var cellIndex = cell.Parent.Cells.IndexOf(cell);

        TagBuilder thisCell =
            cell.Parent.RowType switch
            {
                RowEnum.Header => new TagBuilder("th"),
                _ => new TagBuilder("td"),
            };

        if (cell.Parent.RowType != RowEnum.Header
            && cell.Parent.Table.FixedColumnCount > cellIndex)
        {
            thisCell.AddCssClass("fixedCell");
        }

        thisCell.Attributes.Add("id", cell.Identifier);

        if (cell.Parent != null)
        {
            cell.Parent.Styles/*.Cast<IHtmlStyle>()*/.ToList().ForEach(style =>
            {
                style.Render(thisCell, cell, renderIndex);
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

        var style = cell.GetStyle(renderIndex);
        //cell.Styles.Count > renderIndex
        //    ? (IHtmlStyle)cell.Styles[renderIndex]
        //    : cell.Values.Count == 1
        //        ? (IHtmlStyle)cell.Styles[0]
        //        : null;

        if (style != null)
        {
            style.Render(thisCell, cell, renderIndex);
        }

        //if (cell is ICustomHtmlCell customCell)
        //{
        //    customCell.Render(thisCell, renderIndex, builder);
        //}
        //else
        //{
        thisCell.AddCssClass("align-center");

        switch (cell.GetStyle(renderIndex).TextPosition)
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
        //}

        if (cell.NoWrap)
        {
            //white-space:nowrap;
            var htmlStyle = "white-space:nowrap;" + (thisCell.Attributes.ContainsKey("style") ? thisCell.Attributes["style"] : string.Empty);
            thisCell.MergeAttribute("style", htmlStyle, true);
        }

        if (cell.CanPopup(renderIndex))
        {
            thisCell.Attributes.Add("data-toggle", "popover");
            thisCell.Attributes.Add("data-trigger", "hover");
            thisCell.Attributes.Add("data-content", cell.Values[renderIndex].PopupText);
            thisCell.Attributes.Add("data-original-title", cell.Values[renderIndex].PopupTitle);
        }

        return thisCell;
    }
}