using w4TR1x.ViewTable.Base;
using w4TR1x.ViewTable.Enums;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w4TR1x.ViewTable
{
    public abstract class Cell : EntryCell
    {
        public Cell(string text) : base(text)
        {

        }

        public override TagBuilder Render(TagBuilder builder = null)
        {
            TagBuilder thisCell;

            switch (Parent.RowType)
            {
                case RowEnum.Header:
                    thisCell = new TagBuilder("th");
                    break;
                default:
                    thisCell = new TagBuilder("td");
                    break;
            };

            thisCell.Attributes.Add("id", Identifier);

            thisCell.AddCssClass("align-top");

            switch (TextPosition)
            {
                case TextPositionEnum.Center:
                    thisCell.AddCssClass("center");
                    break;
            }

            thisCell.AddCssClass(BackgroundStyleClass);
            thisCell.AddCssClass(FontStyleClass);

            if (Text != null && Text.Any())
            {
                thisCell.InnerHtml.AppendHtml(Text);
                thisCell.Attributes.Add("data-title", Parent.GetTitleFor(this));
            }
            else
            {
                thisCell.AddCssClass("hidden");
            }

            if (CanPopup())
            {
                thisCell.Attributes.Add("data-toggle", "popover");
                thisCell.Attributes.Add("data-trigger", "hover");
                thisCell.Attributes.Add("data-content", PopupText);
                thisCell.Attributes.Add("data-original-title", PopupTitle);
            }

            return thisCell;
        }
    }
}
