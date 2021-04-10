using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.Values
{
    public class DateValue : CellValue<DateTime>
    {
        public DateValue(DateTime value) : base(value)
        {
        }

        public override object AsOrderValue()
        {
            return ((DateTime)Value).ToBinary();
        }

        public override string ToString()
        {
            return ((DateTime)Value).ToString("dd/MM/yyyy");
        }
    }
}
