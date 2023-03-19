namespace w4TR1x.ViewTable.RazorTagHelper.Extensions;

public static class CellStyleExtensions
{
    public static void Render(this CellStyle? style, TagBuilder builder, Cell cell, int pageIndex)
    {
        if (style == null) return;

        if (style.Bold)
        {
            builder.AddCssClass("font-weight-bold");
        }
    }
}
