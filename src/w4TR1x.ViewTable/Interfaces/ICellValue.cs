using System;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.Interfaces
{
    public interface ICellValue : IDisposable
    {
        public object Value { get; }

        abstract string ToString();

        abstract object AsOrderValue();
    }
}
