using System.Text.Json;
using System.Text.Json.Serialization;
using Color = System.Drawing.Color;

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

        var headerStyle = new HeaderStyle();
        var rowStyle = new BasicStyle(Color.Red, textBold: true, htmlClasses: "text-danger");

        var rnd = new Random();

        model.TestTable
            .AddRow(new Row(RowEnum.Header)
                .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Rows") }, style: headerStyle))
                    .UpdateTextPosition(TextPositionEnum.Left)
                .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Col1") }, style: headerStyle))
                .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Col2") }, style: headerStyle))
                .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Col3") }, style: headerStyle))
                .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Col4") }, style: headerStyle))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row1") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row2") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row3") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row4") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row5") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row6") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row7") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new IntValue(rnd.Next(-100, 100)) })))
                .AddRow(new Row()
                    .AddCell(new Cell(values: new List<ICellValue> { new StringValue("Row8") }, style: rowStyle))
                        .UpdateTextPosition(TextPositionEnum.Left)
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Int) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Single) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Double) }))
                    .AddCell(new Cell(values: new List<ICellValue> { new DoubleValue(rnd.NextDouble() * 100, ValueEnum.Triple) }))));

        var jsonSerializeOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        var modelJson = JsonSerializer.Serialize(model, jsonSerializeOptions);

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
