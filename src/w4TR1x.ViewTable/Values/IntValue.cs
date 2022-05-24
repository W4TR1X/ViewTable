using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w4TR1x.ViewTable.Values
{
    public class IntValue : CellValue<int>
    {
        public IntValue(int value) : base(value)
        {
        }


        public static IntValue From(int value)
        {
            return new IntValue(value);
        }


        public override object AsOrderValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
