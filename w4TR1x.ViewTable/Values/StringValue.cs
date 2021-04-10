using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.Values
{
    public class StringValue : CellValue<string>
    {
        private string _value;
        public new string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        
        public StringValue(string value) : base(value)
        {
            _value = value.Replace("\n", "\r\n" );
        }

        public static StringValue Empty()
        {
            return new StringValue(string.Empty);
        }

        public static StringValue[] From(params string[] values)
        {
            var valueList = new List<StringValue>();

            foreach (var value in values)
            {
                valueList.Add(new StringValue(value));
            }

            return valueList.ToArray();
        }

        public override object AsOrderValue()
        {
            return ToString();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
