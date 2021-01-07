using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace w4TR1x.ViewTable.Interfaces
{
    public interface IEntryCellStyle
    {
        public bool CanPopup();

        public string PopupTitle { get; set; }
        public string PopupText { get; set; }

        public IEntryRow Parent { get; set; }

        public string BackgroundStyleClass { get; set; }
        public string FontStyleClass { get; set; }

        public TagBuilder Render(TagBuilder builder = null);
    }
}
