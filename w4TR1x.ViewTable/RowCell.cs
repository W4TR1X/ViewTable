using Microsoft.AspNetCore.Mvc.Rendering;

namespace w4TR1x.ViewTable
{
    public class RowCell : Cell
    {
        public RowCell(string text) : base(text)
        {

        }

        public override TagBuilder Render(TagBuilder builder = null)
        {
            return base.Render(builder);
        }
    }
}
