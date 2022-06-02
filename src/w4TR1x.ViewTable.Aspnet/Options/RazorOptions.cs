namespace w4TR1x.ViewTable.RazorTagHelper.Options;

public class RazorOptions : ITableRendererOptions<TagBuilder>
{
    //public string? Title { get; set; }
    //public string? Author { get; set; }
    //public string? Subject { get; set; }
    //public string? Company { get; set; }
    //public string? Category { get; set; }
    //public DateTime? CreatedAt { get; set; }

    public TagBuilder Initialize(ITableRendererOptions<TagBuilder>? baseOptions)
    {
        var options = baseOptions as RazorOptions;

        var package = new TagBuilder("view-table");
        //package.Workbook.Properties.Title = options?.Title ?? Title;
        //package.Workbook.Properties.Author = options?.Author ?? Author;
        //package.Workbook.Properties.Created = options?.CreatedAt ?? CreatedAt ?? DateTime.Now;
        //package.Workbook.Properties.Subject = options?.Subject ?? Subject;
        //package.Workbook.Properties.Company = options?.Company ?? Company;
        //package.Workbook.Properties.Category = options?.Category ?? Category;

        return package;
    }
}