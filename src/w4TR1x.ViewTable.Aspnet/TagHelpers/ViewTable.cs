using Microsoft.AspNetCore.Razor.TagHelpers;
using w4TR1x.Aspnet.Extensions;
using w4TR1x.ViewTable;

namespace w4TR1x.Aspnet.TagHelpers
{
    [HtmlTargetElement("view-table", Attributes = nameof(Table), TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ViewTableTagHelper : TagHelper
    {
        public Table Table { get; set; }

        public int RenderIndex { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Table == null)
            {
                output.SuppressOutput();
                return;
            }

            output.Content.SetHtmlContent(Table.Render(RenderIndex));
        }
    }
}
