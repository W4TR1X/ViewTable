using w4TR1x.ViewTable.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.Base
{
    public abstract class EntryCell : IEntryCell
    {
        public string Text { get; }

        public TextPositionEnum TextPosition { get; set; }

        public string Identifier { get; }

        public string PopupTitle { get; set; }
        public string PopupText { get; set; }

        public string BackgroundStyleClass { get; set; }
        public string FontStyleClass { get; set; }

        public IEntryRow Parent { get; set; }

        public bool CanPopup()
        {
            return (PopupTitle != null && PopupTitle.Any()) || (PopupText != null && PopupText.Any());
        }

        public bool IsHidden()
        {
            return Text != null && Text.Any();
        }

        public EntryCell(string text, string identifier = "")
        {
            Identifier = (identifier ?? "").Length == 0 ? $"cell_{Guid.NewGuid().ToString().Replace("-", "")}" : identifier;
            Text = text;
        }


        public abstract TagBuilder Render(TagBuilder builder = null);
    }
}
