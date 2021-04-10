using w4TR1x.ViewTable.Enums;
using System;
using System.Linq;
using w4TR1x.ViewTable.Interfaces;
using System.Collections.Generic;

namespace w4TR1x.ViewTable
{
    public class Table
    {
        public bool Responsive { get; set; }
        public string Identifier { get; private set; }
        public List<IRow> Rows { get; private set; }

        public bool UseVerticalTable { get; set; }
        public bool Stripped { get; set; }

        public List<PageModel> Pages { get; private set; }

        public Table(string identifier = null, params PageModel[] pages)
        {
            init(identifier, pages);
        }

        public Table(params PageModel[] pages)
        {
            init(null, pages);
        }

        void init(string identifier = null, params PageModel[] pages)
        {
            UseVerticalTable = true;

            Pages = new List<PageModel>();
            if (pages.Length != 0)
            {
                Pages.AddRange(pages);
            }
            else
            {
                Pages.Add(new PageModel("Default"));
            }

            Rows = new List<IRow>();
            Identifier = identifier != null && identifier.ToString().Length == 0 ? $"row_{Guid.NewGuid().ToString().Replace("-", "")}" : identifier;
        }

        public Table AddRow(IRow row)
        {
            Rows.Add(row);
            return this;
        }

        public void OrderBy(int cellIndex, int renderIndex, bool desc = false)
        {
            Rows.ForEach(row => row.OrderBy(cellIndex, renderIndex, desc));

            if (!desc)
            {
                Rows = Rows.OrderBy(x => x.GetOrderValue(cellIndex, renderIndex)).ToList();
            }
            else
            {
                Rows = Rows.OrderByDescending(x => x.GetOrderValue(cellIndex, renderIndex)).ToList();
            }
        }

        public class PageModel
        {
            public string PageName { get; set; }
            public int OrderBy { get; set; }
            public bool DescendingOrder { get; set; }

            public PageModel(string pageName, int orderBy = -1, bool descendingOrder = false)
            {
                PageName = pageName;
                OrderBy = orderBy;
                DescendingOrder = descendingOrder;
            }
        }
    }
}
