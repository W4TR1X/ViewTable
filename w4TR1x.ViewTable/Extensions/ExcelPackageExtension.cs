//
//EPPlus Extension sample
//Watch cells with IHasExcelCellStyle property
//

//using OfficeOpenXml;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using w4TR1x.ViewTable.Base;
//using w4TR1x.ViewTable.Interfaces;

//namespace w4TR1x.ViewTable.Extensions
//{
//    public static class ExcelPackageExtension
//    {
//        public static ExcelPackage AsExcelPackage(this Table table, string title, string subject)
//        {
//            var package = new ExcelPackage();
//            package.Workbook.Properties.Title = title;
//            package.Workbook.Properties.Author = "";
//            package.Workbook.Properties.Created = DateTime.Now;
//            package.Workbook.Properties.Subject = subject;
//            package.Workbook.Properties.Company = "";
//            package.Workbook.Properties.Category = "";
//            package.Workbook.Properties.Keywords = "";

//            var sheet = package.Workbook.Worksheets.Add("");
//            IterateRow(table, sheet, 1);

//            return package;
//        }

//        private static void IterateRow(IEntryRow row, ExcelWorksheet sheet, int rowIndex)
//        {
//            var cId = 1;
//            foreach (var cell in row.Cells)
//            {
//                sheet.Cells[rowIndex, cId].Value = cell.Text;

//                if (cell.GetType().IsAssignableTo(typeof(IHasExcelCellStyle)))
//                {
//                    ((IHasExcelCellStyle)cell).Style(sheet.Cells[rowIndex, cId]);
//                }

//                cId++;
//            }

//            foreach (var _row in row.Rows)
//            {
//                IterateRow(_row, sheet, ++rowIndex);
//            }
//        }
//    }
//}
