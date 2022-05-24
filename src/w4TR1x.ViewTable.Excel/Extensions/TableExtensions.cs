using OfficeOpenXml;
using System;
using System.Linq;
using w4TR1x.ViewTable.Models;

namespace w4TR1x.ViewTable.Excel
{
    public static class TableExtentions
    {
        private static ExcelPackage CreateExcelPackage(string title, string subject)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = title;
            package.Workbook.Properties.Author = "Mustafa Dağcı - m4TR1x/w4TR1x";
            package.Workbook.Properties.Created = DateTime.Now;
            package.Workbook.Properties.Subject = subject;
            package.Workbook.Properties.Company = "a m4TR1x/w4TR1x company";
            package.Workbook.Properties.Category = "m4TR1x/w4TR1x category";

            return package;
        }

        public static ExcelPackage Render(this Table table, string topInfo, string title, string subject)
        {
            var package = CreateExcelPackage(title, subject);
            if (table != null)
            {
                var renderIndex = 0;
                foreach (var page in table.Pages)
                {
                    table.Render(renderIndex, page, topInfo, package.Workbook.Worksheets.Add(page.PageName));

                    renderIndex++;
                }

                return package;
            }

            return null;
        }

        public static ExcelWorksheet Render(this Table table, int renderIndex, Page page, string topInfo, ExcelWorksheet sheet)
        {
            var cellCount = table.GetFirstRow().CalculateCellArea();

            if (page.OrderBy >= 0)
            {
                table.OrderBy(page.OrderBy, renderIndex, page.DescendingOrder);
            }

            sheet.OutLineApplyStyle = true;
            sheet.OutLineSummaryBelow = false;

            for (int i = 0; i < cellCount; i++)
            {
                sheet.Column(i + 1).Width = 50;
            }

            var rowIndex = 2;

            foreach (var row in table.Rows)
            {
                row.Render(renderIndex, sheet, ref rowIndex);
            }

            sheet.Cells.AutoFitColumns();

            var range = sheet.Cells[1, 1, 1, cellCount];
            range.Merge = true;
            range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            sheet.Cells[1, 1].Value = topInfo;

            for (int i = 0; i < cellCount; i++)
            {
                sheet.Column(i + 1).Width += 1.71; //for autofilter icon
            }

            return sheet;
        }

        public static ExcelPackage Render(string topInfo, string title, string subject, params (Table Table, int RenderIndex, Page Page, string PageName)[] pageTables)
        {
            var package = CreateExcelPackage(title, subject);

            foreach (var pTable in pageTables)
            {
                pTable.Table.Render(pTable.RenderIndex, pTable.Page, topInfo, package.Workbook.Worksheets.Add(pTable.PageName ?? pTable.Page.PageName));
            }

            return package;
        }
    }
}
