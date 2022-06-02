using w4TR1x.ViewTable.Abstract.Renderers;
using w4TR1x.ViewTable.RazorTagHelper.Options;

namespace w4TR1x.ViewTable.RazorTagHelper
{
    public class ViewTableRazorRenderer : TableRenderer<RazorOptions, TagBuilder>
    {
        public override Task<TagBuilder?> Render(Table table, int pageIndex, RazorOptions? rendererOptions)
        {
            if (Content != null)
            {
                Content = null;
            }

            Content = rendererOptions?.Initialize(DefaultOptions);

            // do tagrender

            return Task.FromResult(Content);
        }
    }
}
