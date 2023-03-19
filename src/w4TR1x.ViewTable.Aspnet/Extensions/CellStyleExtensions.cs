namespace w4TR1x.ViewTable.RazorTagHelper.Extensions;

public static class CellStyleExtensions
{
    public static void Render(this CellStyle? style, TagBuilder builder, Cell cell, int pageIndex)
    {
        /*
        TODO: Need to create style tag in table for all styles,
        This will allow to not duplicate style for cells
            
        <table>
            <style>
                .style-1 {
                    background-color: rgb(150, 150, 0);
                }

                .style-2 {
                    color: rgb(50, 10, 0);
                }
            </style>
            <tr class="style-1">
                <td class="style-2">
                    Hebele
                </td>
            </tr>
        </table>

        */

        if (style == null) return;

        if (style.Bold)
        {
            builder.AddCssClass("font-weight-bold");
        }
    }
}
