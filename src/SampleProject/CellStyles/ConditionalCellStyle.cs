using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using w4TR1x.ViewTable.Interfaces;
using Color = System.Drawing.Color;

namespace SampleProject.CellStyles
{
    public class ConditionalCellStyle : ValueCellStyle
    {
        public override string NumberFormat => null;

        Color redFontColor = Color.FromArgb(217, 37, 80);
        Color greenFontColor = Color.FromArgb(58, 196, 125);

        Color redBgColor = Color.FromArgb(220, 53, 69);
        Color greenBgColor = Color.FromArgb(40, 167, 69);

        public int PositiveZero { get; set; }
        public int NegativeZero { get; set; }

        private readonly bool _isBgColor;
        public ConditionalCellStyle(bool isBgColor)
        {
            _isBgColor = isBgColor;
        }

        public ConditionalCellStyle()
        {

        }

        protected override void RenderExcelStyle(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue)
        {
            base.RenderExcelStyle(selectedRange, row, cell, cellValue);

            if (cellValue == null) return;

            var value = calculateCellValue(cellValue);

            if (value > PositiveZero)
            {
                if (!_isBgColor)
                {
                    selectedRange.Style.Font.Color.SetColor(greenFontColor);
                }
                else
                {
                    selectedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    selectedRange.Style.Fill.BackgroundColor.SetColor(greenBgColor);
                }
            }
            else if (value < NegativeZero)
            {
                if (!_isBgColor)
                {
                    selectedRange.Style.Font.Color.SetColor(redFontColor);
                }
                else
                {
                    selectedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    selectedRange.Style.Fill.BackgroundColor.SetColor(redBgColor);
                }
            }
        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, ICell cell, ICellValue cellValue)
        {
            base.RenderHtmlStyle(tagBuilder, cell, cellValue);

            if (cellValue == null) return;

            var value = calculateCellValue(cellValue);

            if (value > PositiveZero)
            {
                if (!_isBgColor)
                {
                    tagBuilder.AddCssClass("text-success");
                }
                else
                {
                    tagBuilder.AddCssClass("bg-success");
                }
            }
            else if (value < NegativeZero)
            {
                if (!_isBgColor)
                {
                    tagBuilder.AddCssClass("text-danger");
                }
                else
                {
                    tagBuilder.AddCssClass("bg-danger");
                }
            }
        }
    }
}
