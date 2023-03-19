using System.Text.Json;
using System.Text.Json.Serialization;

namespace SampleProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    TableVM prepareSampleViewmodel()
    {
        var model = new TableVM()
        {
            TestTable = new Table(new[]
            {
                new Page("First Table", 4, true)
            })
            {
                Responsive = true,
                UseVerticalTable = true
            }
        };

        //var headerStyle = new HeaderStyle();
        //var rowStyle = new BasicStyle(System.Drawing.Color.Red, textBold: true, htmlClasses: "text-danger");

        var headerStyle = new CellStyle()
        {
            BackgroundColor = new w4TR1x.ViewTable.Models.Color(150, 150, 0)
        };

        var rowStyle = new CellStyle()
        {
            FontColor = new w4TR1x.ViewTable.Models.Color(255, 0, 0),
            Bold = true,
            TextPosition = TextPositionEnum.Center
        };

        var rnd = new Random();

        model.TestTable
            .AddRow(new Row(RowEnum.Header)
                .AddCell(new Cell(values: new List<CellValue> { new StringValue("Rows") }, style: headerStyle))
                    .UpdateTextPosition(TextPositionEnum.Left)
                .AddCell(new Cell(values: new List<CellValue> { new StringValue("Col1") }, style: headerStyle))
                .AddCell(new Cell(values: new List<CellValue> { new StringValue("Col2") }, style: headerStyle))
                .AddCell(new Cell(values: new List<CellValue> { new StringValue("Col3") }, style: headerStyle))
                .AddCell(new Cell(values: new List<CellValue> { new StringValue("Col4") }, style: headerStyle))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row1") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row2") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row3") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row4") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row5") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row6") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row7") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<CellValue> { new StringValue("Row8") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<CellValue> { new DecoratedDoubleValue(rnd.NextDouble() * 100, null, "%", ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<CellValue> { new TimeValue(TimeSpan.FromSeconds(5)) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DateValue(DateTime.Now) }))
                    .AddCell(new Cell(values: new List<CellValue> { new DateTimeValue(DateTime.Now) }))));

        model.TestTable.GetFirstRow().Collapsable = true;
        model.TestTable.GetFirstRow().Collapsed = true;

        var jsonSerializeOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            //WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        };

        var modelJson = JsonSerializer.Serialize(model.TestTable, jsonSerializeOptions);

        ViewBag.Json = modelJson;

        var modelFromJson = JsonSerializer.Deserialize<Table>(modelJson, jsonSerializeOptions);

        ViewBag.modelFromJson = modelFromJson;

        return model;
    }

    public IActionResult Index()
    {
        return View(prepareSampleViewmodel());
    }

    public IActionResult ExcelExport()
    {
        return prepareSampleViewmodel().TestTable.ToExcelFileResult("Sample top row header text", "Title of File", "Subject of file");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}