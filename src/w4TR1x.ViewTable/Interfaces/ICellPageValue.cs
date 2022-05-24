namespace w4TR1x.ViewTable.Interfaces;

public interface ICellPageValue
{
    ICellValue Value { get; set; }
    ICellStyle? Style { get; set; }

    string? PopupTitle { get; set; }
    string? PopupText { get; set; }

    dynamic AsOrderValue();
}
