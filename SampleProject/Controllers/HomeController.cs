using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using w4TR1x.ViewTable;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new TableVM();



            //Sample Table #1
            //Table is the first row 
            var table = new Table(w4TR1x.ViewTable.Enums.RowEnum.Header);
            table.Responsive = true; // Responsive table

            table.AddCell(new HeaderCell("Id"));
            table.AddCell(new HeaderCell("Name"));
            table.AddCell(new HeaderCell("Surname"));
            table.AddCell(new HeaderCell("Date Of Birth"));

            //Row1
            var row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);

            row.AddCell(new RowCell("0"));
            row.AddCell(new RowCell("John"));
            row.AddCell(new RowCell("Doe"));
            row.AddCell(new RowCell("1900-01-25"));

            table.AddRow(row);

            //Row2
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);

            row.AddCell(new RowCell("1"));
            row.AddCell(new RowCell("Isabel"));
            row.AddCell(new RowCell("Dollar"));
            row.AddCell(new RowCell("2009-10-02"));

            table.AddRow(row);

            //Row3
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);

            row.AddCell(new RowCell("2"));
            row.AddCell(new RowCell("Anthony"));
            row.AddCell(new RowCell("Armstrong"));
            row.AddCell(new RowCell("1685-05-09"));

            table.AddRow(row);

            model.SmallTable = table;



            //Sample Table #2
            //Table is the first row 
            table = new Table(w4TR1x.ViewTable.Enums.RowEnum.Header);

            table.AddCell(new HeaderCell("Id"));
            table.AddCell(new HeaderCell("Name"));
            table.AddCell(new HeaderCell("Surname"));
            table.AddCell(new HeaderCell("Date Of Birth"));

            //Row1
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);
            row.Collapsable = true;
            row.Collapsed = true;

            row.AddCell(new TotalCell("100"));
            row.AddCell(new TotalCell(""));
            row.AddCell(new TotalCell(""));
            row.AddCell(new TotalCell("1 Date"));

            table.AddRow(row);

            //Row1-2
            var row2 = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);
            row2.Collapsable = true;
            row2.Collapsed = true;

            row2.AddCell(new RowCell("101"));
            row2.AddCell(new RowCell("John"));
            row2.AddCell(new RowCell("Doe"));
            row2.AddCell(new RowCell("1900-01-25"));

            row.AddRow(row2);

            //Row2
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);
            row.Collapsable = true;
            row.Collapsed = true;

            row.AddCell(new TotalCell("200"));
            row.AddCell(new TotalCell(""));
            row.AddCell(new TotalCell(""));
            row.AddCell(new TotalCell("1 Date"));

            table.AddRow(row);

            //Row2-1
            row2 = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);
            row2.Collapsable = true;
            row2.Collapsed = true;

            row2.AddCell(new RowCell("201"));
            row2.AddCell(new RowCell("Isabel"));
            row2.AddCell(new RowCell("Dollar"));
            row2.AddCell(new RowCell("2009-10-02"));

            row.AddRow(row2);

            model.CollapsableTable = table;




            //Sample Table #3
            //Table is the first row 
            table = new Table(w4TR1x.ViewTable.Enums.RowEnum.Header);
            table.Responsive = true; // Responsive table

            table.AddCell(new HeaderCell("Id"));
            table.AddCell(new HeaderCell("Name"));
            table.AddCell(new HeaderCell("Surname"));
            table.AddCell(new HeaderCell("Date Of Birth"));
            table.AddCell(new HeaderCell("Name"));
            table.AddCell(new HeaderCell("Surname"));
            table.AddCell(new HeaderCell("Date Of Birth"));
            table.AddCell(new HeaderCell("Name"));
            table.AddCell(new HeaderCell("Surname"));

            //Row1
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);

            row.AddCell(new RowCell("0"));
            row.AddCell(new RowCell("John"));
            row.AddCell(new RowCell("Doe"));
            row.AddCell(new RowCell("1900-01-25"));
            row.AddCell(new RowCell("John"));
            row.AddCell(new RowCell("Doe"));
            row.AddCell(new RowCell("1900-01-25"));
            row.AddCell(new RowCell("John"));
            row.AddCell(new RowCell("Doe"));

            table.AddRow(row);

            //Row2
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);

            row.AddCell(new RowCell("1"));
            row.AddCell(new RowCell("Isabel"));
            row.AddCell(new RowCell("Dollar"));
            row.AddCell(new RowCell("2009-10-02"));
            row.AddCell(new RowCell("Isabel"));
            row.AddCell(new RowCell("Dollar"));
            row.AddCell(new RowCell("2009-10-02"));
            row.AddCell(new RowCell("Isabel"));
            row.AddCell(new RowCell("Dollar"));

            table.AddRow(row);

            //Row3
            row = new Row(w4TR1x.ViewTable.Enums.RowEnum.Record);

            row.AddCell(new RowCell("2"));
            row.AddCell(new RowCell("Anthony"));
            row.AddCell(new RowCell("Armstrong"));
            row.AddCell(new RowCell("1685-05-09"));
            row.AddCell(new RowCell("Anthony"));
            row.AddCell(new RowCell("Armstrong"));
            row.AddCell(new RowCell("1685-05-09"));
            row.AddCell(new RowCell("Anthony"));
            row.AddCell(new RowCell("Armstrong"));

            table.AddRow(row);

            model.LongTable = table;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
