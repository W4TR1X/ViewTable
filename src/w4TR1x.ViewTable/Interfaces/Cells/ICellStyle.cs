namespace w4TR1x.ViewTable.Interfaces.Cells;

public interface ICellStyle
{
    TextPositionEnum? TextPosition { get; set; }

    Color? FontColor { get; set; }
    Color? BackgroundColor { get; set; }
    Color? BorderColor { get; set; }
}