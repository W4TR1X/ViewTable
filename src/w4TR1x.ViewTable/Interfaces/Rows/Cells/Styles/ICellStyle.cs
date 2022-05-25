namespace w4TR1x.ViewTable.Interfaces.Rows.Cells.Styles;

public interface ICellStyle
{
    TextPositionEnum? TextPosition { get; set; }

    Color? FontColor { get; set; }
    Color? BackgroundColor { get; set; }
    Color? BorderColor { get; set; }
}