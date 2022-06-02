namespace SampleProject.CellStyles
{
    public abstract class HexelStyle //: ISheetStyle, IHtmlStyle
    {
        public abstract string NumberFormat { get; }

        #region EXCEL

        public void Render(ExcelRange selectedRange, Cell cell, CellValue cellValue)
        {
            setPredefinedOptions(selectedRange, cell, cellValue);
            RenderExcelStyle(selectedRange, null, cell, cellValue);
        }

        public void Render(ExcelRange selectedRange, Row row, Cell cell, CellValue cellValue)
        {
            setPredefinedOptions(selectedRange, cell, cellValue);
            RenderExcelStyle(selectedRange, row, cell, cellValue);
        }

        void setPredefinedOptions(ExcelRange selectedRange, Cell cell, CellValue cellValue)
        {
            var horizontalAlignment = ExcelHorizontalAlignment.Left;

            //switch (cell.TextPosition)
            //{
            //    case TextPositionEnum.Left:
            //        horizontalAlignment = ExcelHorizontalAlignment.Left;
            //        break;
            //    case TextPositionEnum.Center:
            //        horizontalAlignment = ExcelHorizontalAlignment.Center;
            //        break;
            //    case TextPositionEnum.Right:
            //        horizontalAlignment = ExcelHorizontalAlignment.Right;
            //        break;
            //}

            selectedRange.Style.HorizontalAlignment = horizontalAlignment;

            var numberFormat = "General";

            if (cellValue is DoubleValue doubleValue)
            {
                numberFormat = doubleValue.NumberFormat;
            }

            if (cellValue is DateValue)
            {
                numberFormat = "dd/MM/yyyy";
            }

            if (cellValue is DecoratedDoubleValue decorated)
            {
                if (decorated.BeforeText != null)
                {
                    numberFormat = $"\"{decorated.BeforeText} \"{numberFormat}";
                }

                if (decorated.AfterText != null)
                {
                    numberFormat = $"{numberFormat}\" {decorated.AfterText}\"";
                }
            }

            selectedRange.Style.Numberformat.Format = numberFormat;
        }

        protected abstract void RenderExcelStyle(ExcelRange selectedRange, Row row, Cell cell, CellValue cellValue);

        #endregion

        #region HTML

        public void Render(TagBuilder tagBuilder, Cell cell, CellValue cellValue)
        {
            RenderHtmlStyle(tagBuilder, cell, cellValue);
        }

        public abstract void RenderHtmlStyle(TagBuilder tagBuilder, Cell cell, CellValue cellValue);

        #endregion

        protected decimal calculateCellValue(CellValue cellValue)
        {
            if (cellValue.Value is int intValue)
            {
                return intValue;
            }
            else if (cellValue.Value is double doubleValue)
            {
                return (decimal)doubleValue;
            }
            else if (cellValue.Value is decimal decimalValue)
            {
                return decimalValue;
            }

            return 0;
        }
    }
}
