namespace w4TR1x.ViewTable.Razor.Options;

public class RazorOptions : ITableRendererOptions<object>
{
    //public string? Title { get; set; }
    //public string? Author { get; set; }
    //public string? Subject { get; set; }
    //public string? Company { get; set; }
    //public string? Category { get; set; }
    //public DateTime? CreatedAt { get; set; }

    public object Initialize(ITableRendererOptions<object>? baseOptions)
    {
        var options = baseOptions as RazorOptions;

        var package = new object();
        //package.Workbook.Properties.Title = options?.Title ?? Title;
        //package.Workbook.Properties.Author = options?.Author ?? Author;
        //package.Workbook.Properties.Created = options?.CreatedAt ?? CreatedAt ?? DateTime.Now;
        //package.Workbook.Properties.Subject = options?.Subject ?? Subject;
        //package.Workbook.Properties.Company = options?.Company ?? Company;
        //package.Workbook.Properties.Category = options?.Category ?? Category;

        return package;
    }
}