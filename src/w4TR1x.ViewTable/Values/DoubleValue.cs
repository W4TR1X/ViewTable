using System;
using System.Globalization;
using w4TR1x.ViewTable.Enums;

namespace w4TR1x.ViewTable.Values
{
    public class DoubleValue : CellValue<double>
    {
        private readonly double _originalValue;
        private readonly CultureInfo _cultureInfo;

        public string NumberFormat { get; private set; }

        public DoubleValue(double value, ValueEnum decimalCount = ValueEnum.Int, ValueEnum zeroDecimalCount = ValueEnum.Triple,
                            bool displayThousandSeparator = false, bool alwaysDisplayDecimals = false) : base(Math.Round(value, (int)decimalCount))
        {
            _cultureInfo = new CultureInfo("tr-TR", false);

            _originalValue = value;

            NumberFormat = "0";

            var integerPart = (int)Math.Truncate(_originalValue);
            var decimalPart = (float)(_originalValue - integerPart);

            int tmpValue;

            if (integerPart == 0 || alwaysDisplayDecimals)
            {
                switch (zeroDecimalCount)
                {
                    case ValueEnum.Triple:
                        tmpValue = (int)(decimalPart * 1000);
                        if ((tmpValue <= -1 && tmpValue >= -9) || (tmpValue >= 1 && tmpValue <= 9) || alwaysDisplayDecimals)
                        {
                            _cultureInfo.NumberFormat.NumberDecimalDigits = 3;
                            NumberFormat = "0.000";
                            break;
                        }
                        goto case ValueEnum.Double;
                    case ValueEnum.Double:
                        tmpValue = (int)(decimalPart * 100);
                        if ((tmpValue <= -1 && tmpValue >= -9) || (tmpValue >= 1 && tmpValue <= 9) || alwaysDisplayDecimals)
                        {
                            _cultureInfo.NumberFormat.NumberDecimalDigits = 2;
                            NumberFormat = "0.00";
                            break;
                        }
                        goto case ValueEnum.Single;
                    case ValueEnum.Single:
                        tmpValue = (int)(decimalPart * 10);
                        if ((tmpValue <= -1 && tmpValue >= -9) || (tmpValue >= 1 && tmpValue <= 9) || alwaysDisplayDecimals)
                        {
                            _cultureInfo.NumberFormat.NumberDecimalDigits = 1;
                            NumberFormat = "0.0";
                            break;
                        }
                        goto default;
                    default:
                        _cultureInfo.NumberFormat.NumberDecimalDigits = 0;
                        NumberFormat = "0";
                        break;
                }
            }
            else
            {
                switch (decimalCount)
                {
                    case ValueEnum.Triple:
                        NumberFormat = "0.000";
                        break;
                    case ValueEnum.Double:
                        NumberFormat = "0.00";
                        break;
                    case ValueEnum.Single:
                        NumberFormat = "0.0";
                        break;
                    case ValueEnum.Int:
                        NumberFormat = "0";
                        break;
                }
                _cultureInfo.NumberFormat.NumberDecimalDigits = (int)decimalCount;
            }

            _cultureInfo.NumberFormat.NumberGroupSeparator = "";
            if (displayThousandSeparator)
            {
                _cultureInfo.NumberFormat.NumberGroupSeparator = ".";
                NumberFormat = "#,##" + NumberFormat;
            }

            Value = Math.Round(value, _cultureInfo.NumberFormat.NumberDecimalDigits);
        }

        public static DoubleValue Display3DecimalsWhenZeroWithSeparator(double value)
        {
            return new DoubleValue(value, ValueEnum.Int, ValueEnum.Triple, true);
        }

        public override object AsOrderValue()
        {
            return _originalValue;
        }
        public override string ToString()
        {
            return ((double)Value).ToString("N", _cultureInfo);
        }
    }
}
