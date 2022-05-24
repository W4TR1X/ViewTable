using Microsoft.AspNetCore.Mvc;
using w4TR1x.ViewTable;
using w4TR1x.ViewTable.Excel;
using w4TR1x.ViewTable.Models;
using System;

namespace SampleProject.Extensions;

public static class TableExtension
{
    public static FileContentResult ToExcelFileResult(this Table table, string HeaderText, string title, string subject)
    {
        var date = DateTime.Now;

        var fileByteArray = table.Render(HeaderText, title, subject).GetAsByteArray();

        return new FileContentResult(fileByteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"{title} - {date:yyyyMMddhhmmss}.xlsx",
            LastModified = date
        };
    }

    public static FileContentResult ToExcelFileResult(string HeaderText, string title, string subject,
        params (Table Table, int RenderIndex, Page Page, string PageName)[] pageTables)
    {
        var date = DateTime.Now;

        var fileByteArray = TableExtentions.Render(HeaderText, title, subject, pageTables).GetAsByteArray();

        return new FileContentResult(fileByteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"{title} - {date:yyyyMMddhhmmss}.xlsx",
            LastModified = date
        };
    }
}
