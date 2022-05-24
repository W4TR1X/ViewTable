using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using w4TR1x.ViewTable.Interfaces;
using Color = System.Drawing.Color;

namespace SampleProject.CellStyles
{
    public class BasicStyle : ValueCellStyle
    {
        private readonly Color? _bgColor;
        private readonly Color? _fontColor;
        private readonly string[] _htmlClasses;

        public BasicStyle(Color? fontColor = null, Color? bgColor = null, bool textBold = false, params string[] htmlClasses)
        {
            _bgColor = bgColor;
            _textBold = textBold;
            _fontColor = fontColor;
            _htmlClasses = htmlClasses;
        }

        protected override void RenderExcelStyle(ExcelRange selectedRange, IRow row, ICell cell, ICellValue cellValue)
        {
            base.RenderExcelStyle(selectedRange, row, cell, cellValue);

            if (!IsCellEmpty(cellValue))
            {
                if (_bgColor.HasValue)
                {
                    selectedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    selectedRange.Style.Fill.BackgroundColor.SetColor(_bgColor.Value);
                }

                if (_fontColor.HasValue) selectedRange.Style.Font.Color.SetColor(_fontColor.Value);
            }

        }

        public override void RenderHtmlStyle(TagBuilder tagBuilder, ICell cell, ICellValue cellValue)
        {
            base.RenderHtmlStyle(tagBuilder, cell, cellValue);


            if (IsCellEmpty(cellValue))
            {
                tagBuilder.AddCssClass("hidden");
            }
            else
            {
                foreach (var htmlClass in _htmlClasses)
                {
                    tagBuilder.AddCssClass(htmlClass);
                }
            }
        }

        static bool IsCellEmpty(ICellValue cellValue)
        {
            var value = cellValue.ToString();

            value = value == "0" ? "" :
                    value == "0,0" ? "" :
                    value == "0,00" ? "" :
                    value == "0,000" ? "" :
                    value == "0" ? "" :
                    value == "0.0" ? "" :
                    value == "0.00" ? "" :
                    value == "0.000" ? "" : value;

            return !(value != null && value != "");
        }
    }
}
