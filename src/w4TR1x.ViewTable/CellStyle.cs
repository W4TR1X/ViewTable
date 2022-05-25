using w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

namespace w4TR1x.ViewTable
{
    public class CellStyle : ICellStyle
    {
        public TextPositionEnum? TextPosition { get; set; }
        public Color? FontColor { get; set; }
        public Color? BackgroundColor { get; set; }
        public Color? BorderColor { get; set; }
    }
}
