using System;
using w4TR1x.ViewTable.Enums;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.ViewTable.Values
{
    public abstract class CellValue<T> : ICellValue
    {
        public object Value { get; set; }

        public Type valueType;

        public CellValue(T value)
        {
            valueType = value.GetType();
            Value = value;
        }

        public abstract string ToString();
        public abstract object AsOrderValue();

        public void Dispose()
        {

        }
    }
}
